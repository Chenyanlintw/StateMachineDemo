using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject TargetPlayer;
    private Vector3 offset;
    
    void Start()
    {
        // 遊戲開始時，計算與玩家的座標差
        offset = transform.position - TargetPlayer.transform.position;
    }

    
    void Update()
    {
        // 跟隨玩家
        transform.position = Vector3.Lerp(transform.position, TargetPlayer.transform.position + offset, 0.05f);
    }
}
