using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    [SerializeField] EnemyController _enemyController;
    public int Damage = 2;

    public void FinishAttack()
    {
        _enemyController.isAttackFinished = true;
    }
}
