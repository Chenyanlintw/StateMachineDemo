using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody rb;
    private Vector3 startPos;

    void Start()
    {
        // 記錄開始的位置，用於計算飛行距離
        startPos = transform.position;
    }

    void Update()
    {
        // 飛行超過一定距離就刪除
        if (Vector3.Distance(startPos, transform.position) > 20f)
        {
            this.Remove();
        }
    }

    // 設定發射方向
    public void SetDirection(Vector3 dir)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * speed;
    }

    // 從場景上刪除
    public void Remove()
    {
        Destroy(gameObject);
    }
}
