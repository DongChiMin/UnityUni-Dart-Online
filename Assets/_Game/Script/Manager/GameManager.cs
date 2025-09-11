using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Ready,
    FireDart,
    ShowingPoint,
}

public class GameManager : Singleton<GameManager>
{
    DartManager dartManager;
    Dart currentDart;
    void Start()
    {
        dartManager = DartManager.Instance;
        OnInit();
    }

    void OnInit()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
