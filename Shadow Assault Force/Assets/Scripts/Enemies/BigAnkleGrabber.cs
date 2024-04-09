using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAnkleGrabber : MonoBehaviour
{
    [SerializeField] GameObject _littelAnkleGrabberPrefab;
    private GameObject _playerGameObject;
    private PlayerController _playerController;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask _player;
    [SerializeField] LayerMask obstacleLayer;

    private int _maxHp = 5;
    public int _currentHp;
    public int Damage = 5;
    public bool enemyIsDead = false;
    public bool isAttacking = false;
    public float detectionRange = 3f;
    public float attackRange = 0.2f;
    public float moveSpeed = 0.5f;
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
        if (isAttackFinished)
        {
            animator.SetBool("isAttacking", false);
        }
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
                _playerGameObject = col.gameObject;
                _playerController = col.GetComponent<PlayerController>();
                break;
            }
        }
    }
    public void MoveTowardsPlayer()
    {
        // Rotate towards the player
        Vector3 direction = (_playerGameObject.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move towards the player
        float distanceToPlayer = Vector3.Distance(transform.position, _playerGameObject.transform.position);
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
            if (_playerGameObject != null)
            {
                isAttackFinished = false;
                animator.SetBool("isAttacking", true);
            }
            else
            {
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
    public void GotHit(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    void InstantiateNewEnemy()
    {
        Instantiate(_littelAnkleGrabberPrefab, transform.position + Vector3.right * 0.2f, transform.rotation);
        Instantiate(_littelAnkleGrabberPrefab, transform.position + Vector3.left * 0.2f, transform.rotation);
    }
    public void Die()
    {
        InstantiateNewEnemy();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerController.TakeMeleeDamage(Damage);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _playerController.TakeMeleeDamage(Damage);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            GotHit(_playerController.Damage);
        }
    }
}
