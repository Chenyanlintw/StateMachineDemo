using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 IdleTarget = Vector3.zero;

    public Player TargetPlayer;
    public int Hp = 100;
    public Image HpBar;
   
    // 初始設定
    void Start()
    {
        // 取得角色控制元件
        agent = GetComponent<NavMeshAgent>();
    }

    // 遊戲迴圈
    void Update()
    {
        
    }

    // 追逐行為
    void Chasing()
    {
        agent.SetDestination(TargetPlayer.transform.position);
    }

    // 閒置行為
    void Idle()
    {
        Debug.Log(Vector3.Distance(transform.position, IdleTarget));
        if (Vector3.Distance(transform.position, IdleTarget) < 2)
        {
            float r = Random.Range(0, Mathf.PI * 2);
            float rx = Mathf.Cos(r) * 4;
            float ry = Mathf.Sin(r) * 4;
            IdleTarget = new Vector3(rx, 0, ry) + transform.position;
        }
        else
        {
            agent.SetDestination(IdleTarget);
        }
    }

    // 躲避行為
    void Escape()
    {
        Vector3 dir = transform.position - TargetPlayer.transform.position;
        agent.SetDestination(transform.position + dir * 3f);
    }

    // 碰撞
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            OnBulletHit(other.GetComponent<Bullet>());
        }
    }

    // 子彈碰撞反應
    private void OnBulletHit(Bullet bullet)
    {
        bullet.Remove();

        // 扣血
        Hp -= 15;
        if (Hp < 0) {
            Hp = 0;
        }
    }
}
