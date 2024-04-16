using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour, IEnemy
{
    private Transform _playerTransform;
    private PlayerController _playerController;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _droppableObjectPrefab;
    [SerializeField] EnemyWeapon _enemyWeapon;
    [SerializeField] LayerMask obstacleLayer;

    private int _maxHp = 7;
    public int _currentHp;
    public int Damage = 7;
    public bool enemyIsDead = false;
    public bool isAttacking = false;
    public float detectionRange = 3f;
    public float attackRange = 2f;
    public float moveSpeed = 1f;
    public float rotationSpeed = 2f;
    private bool isPlayerDetected;

    private void Start()
    {
        _currentHp = _maxHp;
    }
    private void Update()
    {
        if (!PlayerController.IsplayerDead)
        {
            enemeyState();
        }
    }
    private void enemeyState()
    {
        if (!isPlayerDetected)
        {
            DetectPlayer();
        }
        else
        {
            MoveTowardsPlayer();
        }
    }
    public void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Player"))
            {
                isPlayerDetected = true;
                break;
            }
        }
    }
    public void MoveTowardsPlayer()
    {
        // Rotate towards the player
        Vector3 direction = (_playerTransform.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the player
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.transform.position);
        if (distanceToPlayer > attackRange)
        {
            // Check for obstacles in front of the enemy
            if (!IsObstacleInPath())
            {
                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (_playerTransform != null)
            {
                RangeAttackPlayer();
            }
            
            else
            {
                _enemyWeapon.EndShot();
                isAttacking = false;
            }
        }
    }
    public bool IsObstacleInPath()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRange, obstacleLayer))
        {
            return true;
        }
        return false;
    }
    public void RangeAttackPlayer()
    {
        isAttacking = true;
        _enemyWeapon.StartShot();
    }
    public void GotHit(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        PlayerController.EnemyCount--;
        DropObjects();
        Destroy(gameObject);
    }
    void DropObjects()
    {
        // Randomize the number of objects dropped
        //int numObjectsToDrop = Random.Range(1, 6);
        int numObjectsToDrop = 53;

        for (int i = 0; i < numObjectsToDrop; i++)
        {
            
            Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(0.01f, 0.3f), -0.461f, transform.position.z + Random.Range(0.01f,0.3f));
            Instantiate(_droppableObjectPrefab, dropPosition, Quaternion.identity);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            GotHit(_playerController.Damage);
        }
    }

    public void SetPlayer(PlayerController player)
    {
        _playerController = player;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }
}
