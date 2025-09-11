using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartManager : Singleton<DartManager>
{
    [SerializeField] Stack<Dart> darts = new Stack<Dart>();
    [SerializeField] Dart redDartPrefab;
    [SerializeField] Dart greenDartPrefab;
    Dart currentDart;
    [SerializeField] ObjectPooling dartPool;

    [SerializeField] float timeToDestroyHitDart;

    [SerializeField] PlayerController playerController;
    bool isHit;

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;

        for (int i = 0; i < 10; i++)
        {
            //Tạo phi tiêu đỏ
            Dart dart = Instantiate(redDartPrefab, transform);
            dart.gameObject.SetActive(false);
            darts.Push(dart);

            //Tạo phi tiêu xanh
            dart = Instantiate(greenDartPrefab, transform);
            dart.gameObject.SetActive(false);
            darts.Push(dart);
        }

        //Lấy phi tiêu đầu tiên
        ReloadDart();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDart.GetCurrentState() == DartState.Hit && !isHit)
        {
            isHit = true;
            //Hủy phi tiêu đã cắm sau 5 giây
            StartCoroutine(DisableDart(currentDart));

            Invoke(nameof(ReloadDart), 2);
        }
    }

    IEnumerator DisableDart(Dart dart)
    {
        Dart dartToDestroy = dart;
        yield return new WaitForSeconds(timeToDestroyHitDart);
        dartToDestroy.gameObject.SetActive(false);
    }

    public Dart GetCurrentDart()
    {
        return currentDart;
    }

    void ReloadDart()
    {
        currentDart = darts.Pop();
        currentDart.gameObject.SetActive(true);

        CameraManager.Instance.SetTarget(currentDart.gameObject);
        playerController.SetDart(currentDart);

        isHit = false;
    }
}