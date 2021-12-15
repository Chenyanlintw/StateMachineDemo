using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Enemy TargetEnemy;
    public Image Bar;

    private RectTransform rectTrans;

    void Start() {
        rectTrans = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdatePosition();
        UpdateBarWidth();
    }

    // 更新血條座標
    void UpdatePosition() {
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(TargetEnemy.transform.position);
        Vector3 offset = new Vector3(0, -30, 0);
        rectTrans.position = enemyScreenPos + offset;
    }

    // 更新血條長度
    void UpdateBarWidth() {
        float hpScale = TargetEnemy.Hp * 0.01f;
        Bar.rectTransform.localScale = new Vector3(
            hpScale, 
            Bar.rectTransform.localScale.y, 
            Bar.rectTransform.localScale.z);
    }
}
