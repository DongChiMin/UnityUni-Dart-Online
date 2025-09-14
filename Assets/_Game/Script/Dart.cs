using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            ////Hiệu ứng chúi đầu xuống của phi tiêu
            //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.z) * Mathf.Rad2Deg;
            //Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.left);

            //// Giữ nguyên giá trị y của rotation hiện tại
            //Vector3 currentEuler = transform.rotation.eulerAngles;
            //transform.rotation = Quaternion.Euler(
            //    targetRotation.eulerAngles.x,
            //    currentEuler.y,
            //    targetRotation.eulerAngles.z
            //);
            if (rb.velocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

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
        float randomAngleX = UnityEngine.Random.Range(minAngleX, maxAngleX); // lệch theo ngang
        float randomAngleY = UnityEngine.Random.Range(minAngleY, maxAngleY); // lệch theo dọc

        // Quay rotation của phi tiêu.
        Quaternion deviationRotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);
        Vector3 shootDirection = deviationRotation * Vector3.forward;

        //transform.rotation = Quaternion.Euler(randomAngleX, randomAngleY, 0);

        // Bắn theo hướng đã xoay
        rb.AddForce(shootDirection * flyForce, ForceMode.Impulse);

        //Đổi state
        ChangeState(DartState.Flying);
    }

    void CheckScore()
    {
        Ray ray = new Ray(transform.position, transform.forward * -1f);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, LayerMask.GetMask("Darts"));
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.tag == "InnerBullEye")
            {
                Debug.Log("50 diem");

            }
            else if (hit.collider.tag == "OuterBullEye")
            {
                Debug.Log("25 diem");
            }
            else if (hit.collider.tag == "Treble3x")
            {
                Debug.Log("nhan 3 so diem");
            }
            else if (hit.collider.tag == "Treble2x")
            {
                Debug.Log("Nhan 2 so diem");
            }
            else if (hit.collider.tag == "Single")
            {
                Debug.Log("Giu nguyen diem");
            }
            else
            {
                Debug.Log("Nem truot roi");
            }
        }

        ray = new Ray(transform.position, transform.forward * -1f);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 10f, LayerMask.GetMask("Score"));
        if (hit.collider != null)
        {
            Debug.Log("Có điểm r");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        CheckScore();
        ChangeState(DartState.Hit);
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
        Gizmos.DrawRay(transform.position, transform.forward * -10f);
    }
}
