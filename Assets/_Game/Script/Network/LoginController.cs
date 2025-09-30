using System.Collections;
using System.Collections.Generic;
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
        TCPClientToServer.Instance.SendMessageToServer("login;" + usernameInput.text + ";" +  passwordInput.text);
    }
}
