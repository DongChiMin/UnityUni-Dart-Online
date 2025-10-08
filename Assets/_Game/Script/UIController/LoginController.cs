using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginExecute()
    {
        LoginPacket login = new LoginPacket(usernameInput.text, passwordInput.text);
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(login, stream);
    }

    public void GetOnlineUsersExecute()
    {
        GetOnlineUsersPacket packet = new GetOnlineUsersPacket();
        NetworkStream stream = ServerConnection.Instance.GetStream();
        PacketSender.SendPacket(packet, stream);
    }

    public void TryAgain()
    {
        UIManager.Instance.ShowOnly(UIPaneltype.login);
    }
}
