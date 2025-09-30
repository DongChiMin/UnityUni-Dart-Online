using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DartManager dartManager;
    Dart currentDart;

    //Touch Controll
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector2 swipeVector;
    void Start()
    {
        dartManager = DartManager.Instance;
        currentDart = dartManager.GetCurrentDart();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDart != null) {
            GetSwipeVector();

            if (currentDart.GetCurrentState() == DartState.Ready && Input.GetMouseButtonDown(0))
            {
                //currentDart.Shoot();
            }
        }
    }

    private void GetSwipeVector()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    startTouchPosition = Input.GetTouch(0).position;
        //}
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    endTouchPosition = Input.GetTouch(0).position;

        //    swipeVector = endTouchPosition - startTouchPosition;
        //    Debug.DrawLine(Camera.main.ScreenToWorldPoint(startTouchPosition),
        //       Camera.main.ScreenToWorldPoint(endTouchPosition),
        //       Color.green, 2f);
        //    Debug.Log(swipeVector);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            
            //Xử lý input người chơi: chuẩn hóa input: Vuốt dọc hết màn hình thì y có giá trị = 2
            swipeVector = endTouchPosition - startTouchPosition;
            Vector2 normalizedSwipe = new Vector2(swipeVector.x / Screen.width, swipeVector.y / Screen.height) * 2f;

            Debug.Log("Swipe Normalized: " + normalizedSwipe);
            Debug.Log("Normalized Magnitude: " + normalizedSwipe.magnitude);
            Debug.Log("Normalized Direction: " + normalizedSwipe.normalized);

            Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(startTouchPosition.x, startTouchPosition.y, 10f));
            Vector3 worldEnd = Camera.main.ScreenToWorldPoint(new Vector3(endTouchPosition.x, endTouchPosition.y, 10f));
            Debug.DrawLine(worldStart, worldEnd, Color.green, 10f);

        }
    }

    public void SetDart(Dart dart)
    {
        currentDart = dart;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
 
    }
}
