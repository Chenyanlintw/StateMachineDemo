using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum BehaviorState // 列舉
{
    Idle, Chasing, Escape, Recover
}

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 IdleTarget = Vector3.zero;

    public Player TargetPlayer;
    public GameObject HpPool;
    public float Hp = 100;
    public BehaviorState State;
   
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
        Vector3 playerPos = TargetPlayer.transform.position;
        Vector3 myPos = transform.position;
        
        if (State == BehaviorState.Idle) {
            // 判斷是否要變成追逐
            if (Vector3.Distance(playerPos, myPos) < 10) {
                State = BehaviorState.Chasing;
            }
            // 判斷是否要變成補血
            if (Hp < 30) {
                State = BehaviorState.Recover;
            }
            Idle();
        }
        else if (State == BehaviorState.Chasing) {
            // 判斷是否要變成閒置
            if (Vector3.Distance(playerPos, myPos) > 13) {
                State = BehaviorState.Idle;
            }
            // 判斷是否要變成補血
            if (Hp < 30) {
                State = BehaviorState.Recover;
            }
            Chasing();
        }
        else if (State == BehaviorState.Recover) {
            // 判斷是否要變成閒置
            if (Hp > 70) {
                State = BehaviorState.Idle;
            }
            Recover();
        }

        // 在回血範圍內
        if (Vector3.Distance(HpPool.transform.position, transform.position) < 5) {
            if (Hp < 100)
                Hp += 0.05f;
        }
    }

    // 追逐行為
    void Chasing()
    {
        agent.isStopped = false; // 啟動 agent
        agent.SetDestination(TargetPlayer.transform.position);
    }

    // 閒置行為
    void Idle()
    {
        agent.isStopped = true; // 關閉 agent
    }

    // 躲避行為
    void Escape()
    {
        Vector3 dir = transform.position - TargetPlayer.transform.position;
        agent.isStopped = false; // 啟動 agent
        agent.SetDestination(transform.position + dir * 3f);
    }

    // 回血行為
    void Recover()
    {
        agent.isStopped = false; // 啟動 agent
        agent.SetDestination(HpPool.transform.position);
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; 
        GizmosHelper.DrawCircle( transform.position,  10f );

        Gizmos.color = Color.red; 
        GizmosHelper.DrawCircle( transform.position,  13f );
    }

}
