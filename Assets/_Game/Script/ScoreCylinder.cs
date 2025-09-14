using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCylinder : MonoBehaviour
{
    int[] sectorScores = new int[20]
    {
        6, 13, 4, 18, 1,
        20, 5, 12, 9, 14,
        11, 8, 16, 7, 19,
        3, 17, 2, 15, 10
    };

    private void OnTriggerEnter(Collider other)
    {
        Vector3 from = other.transform.position + other.transform.forward * 3f;
        Vector3 direction = other.transform.forward * -1f;


        Debug.DrawRay(from, direction * 6f, Color.red, 1f);
        RaycastHit hit;
        if(Physics.Raycast(from, direction, out hit, 6f, LayerMask.GetMask("Score")))
        {
            Debug.Log("Raycast hit at: " + hit.point);
            CalculateScore(hit.point);
        }
        else
        {
            CalculateScore(other.transform.position);
        }
    }


    void CalculateScore(Vector3 hitPoint)
    {
        Vector3 center = transform.position;
        Vector3 dir = hitPoint - center;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        Debug.Log("Angle:" + angle);

        //Hiện tại trong logic 0 độ --> 18 dộ là ô điểm thứ nhất. Nhưng thực tế -9 độ --> 9 độ mới là ô điểm thứ nhất
        //Giải pháp: offset cộng 9 độ để từ -9 đến 9 là ô điểm thứ nhất
        angle += 9f;
        if (angle > 360) angle -= 360;

        int numberOfSectors = 20;
        float sectorAngle = 360f / numberOfSectors;

        int sectorIndex = Mathf.FloorToInt(angle / sectorAngle);
        int score = sectorScores[sectorIndex]; // Mảng điểm của từng lát pizza

        Debug.Log($"Hit sector {sectorIndex}, Score: {score}");
    }
}
