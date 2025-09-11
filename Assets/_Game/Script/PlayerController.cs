using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DartManager dartManager;
    Dart currentDart;
    void Start()
    {
        dartManager = DartManager.Instance;
        currentDart = dartManager.GetCurrentDart();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDart != null) {
            if (currentDart.GetCurrentState() == DartState.Ready && Input.GetMouseButtonDown(0))
            {
                currentDart.Shoot();
            }
        }
    }

    public void SetDart(Dart dart)
    {
        currentDart = dart;
    }
}
