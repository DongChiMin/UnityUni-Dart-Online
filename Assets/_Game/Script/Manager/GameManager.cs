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
    
    void Start()
    {
        OnInit();
    }

    void OnInit()
    {

    }

    void Update()
    {

    }

    public void SetScore()
    {

    }

    public void SetMulipier()
    {

    }
}
