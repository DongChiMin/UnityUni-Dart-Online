using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

public class ServerConnection : Singleton<ServerConnection>
{
    [SerializeField] string serverIpAddress;
    [SerializeField] int serverPort;

    TcpClient tcpClient;
    NetworkStream stream;

    //Lắng nghe dữ liệu
    private Thread listenThread;
    private bool isRunning = false;

    bool isConnectDone = false;
    private bool isConnectFailed = false;

    // Queue để lưu dữ liệu nhận được, xử lý trên main thread
    private ConcurrentQueue<string> messageQueue = new ConcurrentQueue<string>();

    void Start()
    {
        StartConnect();
    }

    private void Update()
    {
        //Mỗi khi nhận được message từ server -> gọi hàm
        while (messageQueue.TryDequeue(out string msg))
        {
            Debug.Log("Đã nhận dữ liệu" + msg);
            MessageRouter.Instance.Route(msg);
        }

        //Nếu server kết nối thành công hoặc bị dừng chạy -> hiện UI try again
        if(isConnectDone)
        {
            DoneConnect();
        }
    }

    //Bắt đầu connect đến server (dùng cho cả nút try again)
    public void StartConnect()
    {
        StartCoroutine(ConnectWithLoading());
    }

    //Sau khi connect xong, kiểm tra có thành công không để hiện UI
    private void DoneConnect()
    {
        isConnectDone = false;
        UIManager.Instance.HideAll();
        if (isConnectFailed)
        {
            isConnectFailed = false;
            UIManager.Instance.Show(UIPaneltype.connectionFailed);
        }
        else
        {
            UIManager.Instance.Show(UIPaneltype.login);
        }
    }

    //Loading khi đang kết nối server, tạo luồng phụ cho server.
    IEnumerator ConnectWithLoading()
    {
        UIManager.Instance.HideAll();
        UIManager.Instance.Show(UIPaneltype.connecting);

        // Kết nối trong thread riêng để không block
        new Thread(() =>
        {
            ConnectToServer(serverIpAddress, serverPort);
            isConnectDone = true;
        }).Start();

        while (!isConnectDone)
            yield return null;
    }

    //Mở luồng tới server
    void ConnectToServer(string ip, int port)
    {
        try
        {
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            Debug.Log("✅ Đã kết nối đến máy chủ!");

            isRunning = true;
            listenThread = new Thread(ListenToServer);
            listenThread.IsBackground = true;
            listenThread.Start();
            Debug.Log("✅ Đã lắng nghe máy chủ!");
        }
        catch (Exception e)
        {
            isConnectFailed = true;
            Debug.LogError("❌ Không thể kết nối: " + e.Message);
        }
    }

    //Lắng nghe server
    void ListenToServer()
    {
        byte[] buffer = new byte[1024];
        while (isRunning)
        {
            try
            {
                if (stream.CanRead && tcpClient.Available > 0)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        messageQueue.Enqueue(msg);
                    }
                }
                else
                {
                    Thread.Sleep(10); // tránh CPU load cao
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Lỗi khi nhận dữ liệu: " + e.Message);
                isRunning = false;
                isConnectFailed = true;
                isConnectDone = true;
            }
        }
    }

    public NetworkStream GetStream()
    {
        return stream;
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        listenThread?.Join();

        if (stream != null) stream.Close();
        if (tcpClient != null) tcpClient.Close();
    }

}
