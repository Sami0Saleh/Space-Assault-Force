using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed = 10f; // Initial speed of the grenade
    public float arcHeight = 5f; // Height of the arc (peak point)
    public float gravity = 9.81f; // Gravity value (m/s^2)
    public float circleRadius = 0.1f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;
    private float journeyLength;


    private bool isMoving = false;

    public void Launch(Vector3 target)
    {
        startPosition = transform.position;
        targetPosition = target;
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPosition, targetPosition);

        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            // Calculate the current journey time
            float journeyTime = Time.time - startTime;

            if (journeyTime >= 0f && journeyTime <= 1f)
            {
                // Calculate the current position along the arc trajectory
                float height = Mathf.Sin(journeyTime * Mathf.PI) * arcHeight;
                Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, journeyTime) + Vector3.up * height;

                // Update the position of the grenade
                transform.position = currentPos;
            }
        }
            
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;

        Vector3 previousPoint = Vector3.zero;

        for (int i = 1; i <= 36; i++)
        {
            float angle = i * (360f / 36);
            Vector3 newPoint = targetPosition + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward * circleRadius;

            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, newPoint);
            }

            previousPoint = newPoint;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        isMoving = false;
    }
}
