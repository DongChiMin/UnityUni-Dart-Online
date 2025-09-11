using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] GameObject target;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, moveSpeed *  Time.deltaTime);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
