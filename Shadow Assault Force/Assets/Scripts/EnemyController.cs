using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform playerPos;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _droppableObjectPrefab;

    private int _maxHp = 10;
    private int _currentHp;
    public float movmentSpeed = 1f;
    public float rotateSpeed = 0.3f;
    public int Damage = 2;
    public bool enemyIsDead = false;
    public bool isAttacking = false;

    [SerializeField] NavMeshAgent SpitterAgent;
    [SerializeField] LayerMask _player;
    public float SightRange;
    public float AttackRange;
    private bool _playerIsInMySight;
    private bool _playerInAttackRange;


    private void Start()
    {
        _currentHp = _maxHp;
    }
    void Update()
    {
        enemeyState();


    }
    private void enemeyState()
    {
        //Check for Sight and Damage Range
        _playerIsInMySight = Physics.CheckSphere(transform.position, SightRange, _player);
        _playerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, _player);


        if (_playerIsInMySight && !_playerInAttackRange)
        {
            ChasePlayer();
        }

        if (_playerIsInMySight && _playerInAttackRange)
        {
            isAttacking = true;
            animator.SetBool("isWalking", false);
            animator.SetBool("isAttacking", true);
        }
    }
    private void ChasePlayer() // great
    {
        transform.LookAt(playerPos);
        SpitterAgent.SetDestination(playerPos.position);
    }
    public void GotHit()
    {
        _currentHp--;
        Debug.Log("Got Hit By Bullet");
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        DropObjects();
        Destroy(gameObject);
    }

    void DropObjects()
    {
        // Randomize the number of objects dropped
        int numObjectsToDrop = Random.Range(1, 5); // Example: Drop between 1 to 4 objects

        for (int i = 0; i < numObjectsToDrop; i++)
        {
            // Instantiate the droppable object with random position offset
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * 2f; // Example: Drop objects within a 2-unit radius
            Instantiate(_droppableObjectPrefab, dropPosition, Quaternion.identity);
        }
    }
}
