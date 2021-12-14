using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class GizmosHelper
{
    static public void DrawCircle(Vector3 center, float radius, int divides = 30)
    {
        for (int i = 0; i < 30; i++)
        {
            float angle1 = (Mathf.PI * 2) / divides * i;
            float x1 = Mathf.Cos(angle1) * radius;
            float y1 = Mathf.Sin(angle1) * radius;

            float angle2 = (Mathf.PI * 2) / divides * (i + 1);
            float x2 = Mathf.Cos(angle2) * radius;
            float y2 = Mathf.Sin(angle2) * radius;

            Vector3 p1 = new Vector3(x1, 0, y1) + center;
            Vector3 p2 = new Vector3(x2, 0, y2) + center;
            Gizmos.DrawLine(p1, p2);
        }
    }
}
