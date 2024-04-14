using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] GameObject _bullet;

    public void Shoot(Vector3 direction)
    {
        Instantiate(_bullet, transform.position, Quaternion.LookRotation(direction));
    }
}
