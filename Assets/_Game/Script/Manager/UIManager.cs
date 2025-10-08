using System.Collections.Generic;
using UnityEngine;

public enum UIPaneltype
{
    connecting,
    connectionFailed,
    register,
    login,
    loginFailed,
    mainMenu
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject connectingPanel;
    [SerializeField] GameObject registerPanel;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject connectionFailedPanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject loginFailedPanel;
    private Dictionary<UIPaneltype, GameObject> panels = new Dictionary<UIPaneltype, GameObject>();

    void Awake()
    {
        panels.Add(UIPaneltype.connectionFailed, connectionFailedPanel);
        panels.Add(UIPaneltype.connecting, connectingPanel);
        panels.Add(UIPaneltype.register, registerPanel);
        panels.Add(UIPaneltype.login, loginPanel);
        panels.Add(UIPaneltype.mainMenu, mainMenuPanel);
        panels.Add(UIPaneltype.loginFailed, loginFailedPanel);
    }

    public void ShowOnly(UIPaneltype panelType)
    {
        HideAll();
        Show(panelType);
    }

    public void Show(UIPaneltype panelType)
    {
        if (panels.TryGetValue(panelType, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy panel: {panelType}");
        }
    }

    public void Hide(UIPaneltype panelType)
    {
        if (panels.TryGetValue(panelType, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy panel: {panelType}");
        }
    }

    public void HideAll()
    {
        foreach (var panel in panels.Values)
        {
            panel.SetActive(false);
        }
    }
}
