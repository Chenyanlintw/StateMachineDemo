using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletRef;
    public GameObject firePos;

    private float speed = 5f;
    private CharacterController controller;

    void Start()
    {
        // 取得角色控制元件
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Movement()
    {
        // 取得方向鍵輸入取得方向
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);

        // 旋轉到輸入方向
        if (dir.magnitude > 0.1f)
        {
            float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
        }

        // 移動
        controller.Move(dir * speed * Time.deltaTime);
    }

    void Fire()
    {
        Bullet b = Instantiate<Bullet>(bulletRef, firePos.transform.position, Quaternion.Euler(transform.forward));
        b.SetDirection(transform.forward);
    }
}
