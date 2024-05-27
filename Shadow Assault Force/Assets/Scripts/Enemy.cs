using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask _payloadLayer;
    [SerializeField] float _fireRange;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _fireRate;

    void Update()
    {
        agent.destination = target.transform.position;
    }

    public void CheckRange()
    {
       if(Physics.CheckSphere(transform.position, _fireRange, _payloadLayer))
        {
            transform.Rotate(target.transform.position);
            Invoke("Fire", _fireRate);
        }
    }

    public void Fire()
    {
        Instantiate(_bullet);
    }
}
