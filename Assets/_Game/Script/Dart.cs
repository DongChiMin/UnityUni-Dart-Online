using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, force));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DartBoard")
        {
            Debug.Log("Va cham");
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
