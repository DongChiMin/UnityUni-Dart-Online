using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
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
    [SerializeField] float rotationSpeed;

    [Header("Do lech khi nem phi tieu")]
    [Range(-50f, -10f)]
    public float maxAngleX;

    [Range(-50f, -10f)]
    public float minAngleX;

    [Range(-20f, 20f)]
    public float maxAngleY;

    [Range(-20f, 20f)]
    public float minAngleY;

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
            if (rb.velocity != Vector3.zero)
            {
                //Nhìn về hướng velocity (hướng bay) của phi tiêu để quay dần dần về hướng đó
                Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            //Tăng trọng lực cho phi tiêu chúi xuống nhanh hơn
            rb.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
    }

    public void Shoot()
    {
        // Góc lệch ngẫu nhiên
        float randomAngleX = UnityEngine.Random.Range(minAngleX, maxAngleX); // lệch theo ngang
        float randomAngleY = UnityEngine.Random.Range(minAngleY, maxAngleY); // lệch theo dọc

        // Quay rotation của phi tiêu.
        Quaternion deviationRotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);
        Vector3 shootDirection = deviationRotation * Vector3.forward;

        // Bắn theo hướng đã xoay
        rb.AddForce(shootDirection * flyForce, ForceMode.Impulse);

        //Đổi state
        ChangeState(DartState.Flying);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Multiplier"))
        {
            switch (other.tag)
            {
                case "InnerBullEye":
                    Debug.Log("50 diem");
                    break;
                case "OuterBullEye":
                    Debug.Log("25 diem");
                    break;
                case "Treble3x":
                    Debug.Log("Nhan 3 so diem");
                    break;
                case "Treble2x":
                    Debug.Log("Nhan 2 so diem");
                    break;
                case "Single":
                    Debug.Log("Giu nguyen diem");
                    break;
            }
        }
        //Chuyển trạng thái của phi tiêu thành hit
        ChangeState(DartState.Hit);
        Hit();
    }

    void Hit()
    {
        //Khi bắn trúng đích thì dừng lại + tắt trọng lực
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward * -1f;  // ❗️KHÔNG nên dùng trong 2D

        float distance = 0.1f;
        Vector3 end = origin + direction.normalized * distance;

        Gizmos.DrawLine(origin, end);
    }
}
