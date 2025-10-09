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
    private Dictionary<UIPaneltype, GameObject> panels = new Dictionary<UIPaneltype, GameObject>();

    void Awake()
    {
        foreach (Transform child in transform)
        {
            // Giả sử mỗi panel đều có tên trùng với Enum
            if (System.Enum.TryParse(child.name, out UIPaneltype type))
            {
                panels[type] = child.gameObject;
            }
            else
            {
                Debug.LogWarning($"Panel name '{child.name}' không khớp với UIPaneltype enum.");
            }
        }
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
