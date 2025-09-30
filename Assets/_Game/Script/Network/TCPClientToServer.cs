using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;

public class TCPClientToServer : Singleton<TCPClientToServer>
{
    [SerializeField] string serverIpAddress;
    [SerializeField] int serverPort;

    TcpClient tcpClient;
    NetworkStream stream;

    void Start()
    {
        ConnectToServer(serverIpAddress, serverPort);   
    }

    void ConnectToServer(string ip, int port)
    {
        try
        {
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            Debug.Log("✅ Đã kết nối đến máy chủ!");
        }
        catch (Exception e)
        {
            Debug.LogError("❌ Không thể kết nối: " + e.Message);
        }
    }

    public void SendMessageToServer(string message)
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            Debug.LogWarning("⚠️ Chưa kết nối đến server.");
            return;
        }

        byte[] data = Encoding.UTF8.GetBytes(message + "\n");
        stream.Write(data, 0, data.Length);
        Debug.Log("📤 Đã gửi: " + message);
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (tcpClient != null) tcpClient.Close();
    }

}
