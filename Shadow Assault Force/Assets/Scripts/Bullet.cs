using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Rigidbody rb;

    void Update()
    {
        rb.MovePosition(transform.forward * Time.deltaTime);
    }
}
