using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletSpawner : MonoBehaviour
{

    [SerializeField] GameObject _bullet;
    public float _weaponTime;
    
    public void FireWeapons()
    {
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(_weaponTime);
        Vector3 startPos = transform.position;
        Quaternion startRot = new Quaternion(0f, 0f, 0f, 0f);
        Instantiate(_bullet, startPos, startRot);
    }
}
