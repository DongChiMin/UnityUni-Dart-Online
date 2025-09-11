using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;

    [Tooltip("The duration until gameObject return to Pool (-1: Never)")]
    [Range(-1.1f, 100)]
    [SerializeField] private float timeToAddPool;

    private Queue<GameObject> poolQueue = new Queue<GameObject>();
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            poolQueue.Enqueue(obj);
        }
    }


    public GameObject GetFromPool(Vector3 position, Quaternion rotation, Vector3 localScale)
    {
        GameObject obj;

        //Neu khong co Object trong pool --> Instantiate
        if (poolQueue.Count > 0)
        {
            obj = poolQueue.Dequeue();
            obj.SetActive(false);
        }
        else
        {
            obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
        }

        //thiet lap vi tri
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.localScale = localScale;
        obj.gameObject.SetActive(true);

        //Sau khoang thoi gian, tra Object vao lai pool
        if (timeToAddPool > 0f) StartCoroutine(AddToPoolDelay(obj, timeToAddPool));
        return obj;
    }

    IEnumerator AddToPoolDelay(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        if (!poolQueue.Contains(obj) && obj.activeSelf == true)
        {
            AddToPool(obj);
        }

    }

    public void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
}
