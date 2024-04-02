using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    private GameObject _playerController;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _droppableObjectPrefab;
    [SerializeField] EnemyWeapon _enemyWeapon;
    [SerializeField] LayerMask _player;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] bool _isRanged;
    [SerializeField] bool _isBigAnkleGrabber;

    private int _maxHp = 10;
    public int _currentHp;
    public int Damage = 2;
    public bool enemyIsDead = false;
    public bool isAttacking = false;
    public float detectionRange = 3f;
    public float attackRange = 2f;
    public float moveSpeed = 1f;
    public float rotationSpeed = 2f;

    public bool isAttackFinished = true;
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
        if(isAttackFinished)
        {
            animator.SetBool("isAttaking", false);
        }
        if (!isPlayerDetected)
        {
            // Detect the player
            DetectPlayer();
        }
        else
        {
            // Move towards the player
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
                _playerController = col.gameObject;
                break;
            }
        }
    }
    public void MoveTowardsPlayer()
    {
        
        // Rotate towards the player
        Vector3 direction = (_playerController.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the player
        float distanceToPlayer = Vector3.Distance(transform.position, _playerController.transform.position);
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
            if (_isRanged && _playerController != null)
            {
                RangeAttackPlayer();
            }
            else if (!_isRanged && _playerController != null)
            {
                isAttackFinished = false;
                animator.SetBool("isAttaking", true);
            }
            else
            {
                _enemyWeapon._isShooting = false;
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
        _enemyWeapon._isShooting = true;
    }
    
    public void GotHit()
    {
        _currentHp--;
        if (_currentHp <= 0)
        {
            if (!_isBigAnkleGrabber)
            {
                Die();
            }
        }
    }
    public void Die()
    {
        if (!_isBigAnkleGrabber)
        {
            PlayerController.EnemyCount--;
            DropObjects();
        }
        Destroy(gameObject);
    }

    void DropObjects()
    {
        // Randomize the number of objects dropped
        int numObjectsToDrop = Random.Range(1, 5); // Example: Drop between 1 to 4 objects

        for (int i = 0; i < numObjectsToDrop; i++)
        {
            // Instantiate the droppable object with random position offset
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * 0.3f; // Example: Drop objects within a 2-unit radius
            Instantiate(_droppableObjectPrefab, dropPosition, Quaternion.identity);
        }
    }
    /*public void SetPlayer(PlayerController player)
    {
        _playerController = player;
    }*/
    
}
