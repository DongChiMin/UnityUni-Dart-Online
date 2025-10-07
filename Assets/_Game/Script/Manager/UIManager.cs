using System.Collections.Generic;
using UnityEngine;

public enum UIPaneltype
{
    connecting,
    connectionFailed,
    register,
    login
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject connectingPanel;
    [SerializeField] GameObject registerPanel;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject connectionFailedPanel;
    private Dictionary<UIPaneltype, GameObject> panels = new Dictionary<UIPaneltype, GameObject>();

    void Awake()
    {
        panels.Add(UIPaneltype.connectionFailed, connectionFailedPanel);
        panels.Add(UIPaneltype.connecting, connectingPanel);
        panels.Add(UIPaneltype.register, registerPanel);
        panels.Add(UIPaneltype.login, loginPanel);
    }

    public void Show(UIPaneltype panelName)
    {
        if (panels.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy panel: {panelName}");
        }
    }

    public void Hide(UIPaneltype panelName)
    {
        if (panels.TryGetValue(panelName, out GameObject panel))
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy panel: {panelName}");
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
