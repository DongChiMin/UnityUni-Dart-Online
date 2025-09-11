using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DartState
{
    Ready,
    Flying,
    Hit
}

public class Dart : MonoBehaviour
{
    [Header("Thuoc tinh")]
    [SerializeField] float flyForce;
    [SerializeField] float gravityForce;

    Rigidbody rb;
    private DartState dartState;
    public event Action<DartState> OnStateChanged;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        OnInit();
    }

    void OnInit()
    {
        rb.useGravity = false;
        ChangeState(DartState.Ready);
    }

    private void FixedUpdate()
    {
        if(dartState == DartState.Flying)
        {
            //Hiệu ứng chúi đầu xuống của phi tiêu
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.left);

            // Giữ nguyên giá trị y của rotation hiện tại
            Vector3 currentEuler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(
                targetRotation.eulerAngles.x,
                currentEuler.y,
                targetRotation.eulerAngles.z
            );

            //Tăng trọng lực cho phi tiêu chúi xuống nhanh hơn
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
        else if(dartState == DartState.Hit) {
            //Khi bắn trúng đích thì dừng lại + tắt trọng lực
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }

    public void Shoot()
    {
        //rb.AddForce((transform.forward + transform.right + transform.up) * flyForce, ForceMode.Impulse);
        //ChangeState(DartState.Flying);

        // Góc lệch ngẫu nhiên
        float randomAngleX = UnityEngine.Random.Range(-25f, 25f); // lệch theo ngang
        float randomAngleY = UnityEngine.Random.Range(-25f, 25f); // lệch theo dọc

        // Quay rotation của phi tiêu.
        transform.rotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);

        // Bắn theo hướng đã xoay
        rb.AddForce(transform.forward * flyForce, ForceMode.Impulse);

        //Đổi state
        ChangeState(DartState.Flying);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DartBoard")
        {
            Debug.Log("Va cham");
            ChangeState(DartState.Hit);
        }
    }

    void ChangeState(DartState newState) 
    {
        dartState = newState;
        OnStateChanged?.Invoke(dartState);
    }

    public DartState GetCurrentState()
    {
        return dartState;
    }
}
