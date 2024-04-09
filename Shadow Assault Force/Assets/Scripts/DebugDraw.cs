using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugDraw
{
    public static void Circle(Vector3 center, Vector3 up, Color color, float radius, int segments = 36)
    {
        float angle = 0f;
        Vector3 previousPoint = center + Quaternion.AngleAxis(0f, up) * Vector3.forward * radius;

        for (int i = 1; i <= segments; i++)
        {
            angle = i * (360f / segments);
            Vector3 newPoint = center + Quaternion.AngleAxis(angle, up) * Vector3.forward * radius;
            Debug.DrawLine(previousPoint, newPoint, color);
            previousPoint = newPoint;
        }
    }
}
