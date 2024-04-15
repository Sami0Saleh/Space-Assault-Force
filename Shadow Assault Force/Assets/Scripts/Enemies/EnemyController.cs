using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    [SerializeField] List<IEnemy> _enemies = new List<IEnemy>();
    [SerializeField] PlayerController _playerController;
    [SerializeField] Transform _playerTransform;

    private void Awake()
    {
    }
    private void Update()
    {
        _enemies.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<IEnemy>());

        foreach (var enemy in _enemies)
        {
            enemy.SetPlayer(_playerController);
            enemy.SetPlayerTransform(_playerTransform);
        }
    }


}
