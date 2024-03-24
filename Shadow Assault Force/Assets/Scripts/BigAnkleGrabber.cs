using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAnkleGrabber : MonoBehaviour
{
    [SerializeField] EnemyController _enemyController;
    [SerializeField] GameObject _littelAnkleGrabberPrefab;
    [SerializeField] PlayerController _playerController;

    private void Update()
    {
        if (_enemyController._currentHp == 0)
        {
            InstantiateNewEnemy();
            _enemyController.Die();
        }
    }

    void InstantiateNewEnemy()
    {
        /*GameObject newEnemy =*/ Instantiate(_littelAnkleGrabberPrefab, transform.position + Vector3.right * 0.2f, transform.rotation);
        /*GameObject newEnemy1 =*/ Instantiate(_littelAnkleGrabberPrefab, transform.position + Vector3.left * 0.2f, transform.rotation);
       /* EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        EnemyController newEnemyController1 = newEnemy1.GetComponent<EnemyController>();


        if (newEnemyController != null && newEnemyController1 != null)
        {
            newEnemyController.SetPlayer(_playerController);
            newEnemyController1.SetPlayer(_playerController);
        }*/
    }
    /*private void Update()
    {
        if (_enemyController._currentHp == 0)
        {
            Instantiate(_littelAnkleGrabber, new Vector3(transform.position.x +0.2f, -0.460641f, 0), transform.rotation);
            //Instantiate(_littelAnkleGrabber, new Vector3(transform.position.x - 0.2f, -0.460641f, 0), transform.rotation);

            if (_playerController != null)
            {
                _enemyController.SetPlayer(_playerController);
            }
            _enemyController.Die();
        }
    }*/
}
