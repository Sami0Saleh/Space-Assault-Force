using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private float _speed = 2f;
    private GameObject _playerGameObject;

    public void CollectCoins()
    {
        transform.Translate(_playerGameObject.transform.position * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            _playerGameObject = other.gameObject;
        }
    }
}
