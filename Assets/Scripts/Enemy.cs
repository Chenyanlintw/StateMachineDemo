using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum BehaviorState // 列舉
{
    Idle, Chasing, Escape
}

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 IdleTarget = Vector3.zero;

    public Player TargetPlayer;
    public int Hp = 100;
    public BehaviorState State = BehaviorState.Idle; // 狀態
   
    // 初始設定
    void Start()
    {
        // 取得角色控制元件
        agent = GetComponent<NavMeshAgent>();
        State = BehaviorState.Idle;
    }

    // 遊戲迴圈
    void Update()
    {
        if (State == BehaviorState.Idle) {

            // 判斷是否要變成追逐
            if (Vector3.Distance(TargetPlayer.transform.position, transform.position) < 5) {
                State = BehaviorState.Chasing;
            }

            Idle();
        }
        else if (State == BehaviorState.Chasing) {

            // 判斷是否要變成閒置
            if (Vector3.Distance(TargetPlayer.transform.position, transform.position) > 10) {
                State = BehaviorState.Idle;
            }
            Chasing();
        }
        else if (State == BehaviorState.Escape) {
            Escape();
        }
    }

    // 追逐行為
    void Chasing()
    {
        agent.isStopped = false;
        agent.SetDestination(TargetPlayer.transform.position);
    }

    // 閒置行為
    void Idle()
    {
        agent.isStopped = true;
        /*  
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
        }*/
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
