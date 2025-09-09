using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, moveSpeed *  Time.deltaTime);
    }
}
