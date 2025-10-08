using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler
{
    public LoginHandler()
    {

    }

    public void Handle(ResponseData data)
    {
        string status = data.status;
        switch (status)
        {
            case "SUCCESS":
                UIManager.Instance.ShowOnly(UIPaneltype.mainMenu);
                break;
            case "FAIL":
                UIManager.Instance.Show(UIPaneltype.loginFailed);
                break;
            default:
                Debug.Log("Lỗi status của dữ liệu:" + data);
                break;
        }
    }
}
