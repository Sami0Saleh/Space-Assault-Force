using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    public float speed = 10f; // Initial speed of the grenade
    public float arcHeight = 5f; // Height of the arc (peak point)

    private Vector3 startPos;
    private Vector3 targetPos;
    private float startTime;

    // Set the target position and start the trajectory calculation
    public void Shoot(Vector3 target)
    {
        startPos = transform.position;
        targetPos = target;

        startTime = Time.time;

        // Calculate the required velocity and launch angle for the arc trajectory
        Vector3 direction = targetPos - startPos;
        float distance = direction.magnitude;
        float yOffset = arcHeight - (0.5f * Physics.gravity.y * Mathf.Pow(distance / speed, 2f));

        float launchAngle = Mathf.Atan((speed * speed + Mathf.Sqrt(speed * speed * speed * speed - Physics.gravity.magnitude * yOffset * distance)) / (Physics.gravity.magnitude * distance));

        Vector3 velocity = direction / (distance * Mathf.Cos(launchAngle));
        velocity.y = Mathf.Sin(launchAngle) * distance;

        // Apply initial velocity to the Rigidbody
        _rb.velocity = velocity * speed;
    }
    /*private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }*/
}
