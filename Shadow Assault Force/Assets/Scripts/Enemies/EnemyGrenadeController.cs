using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrenadeController : MonoBehaviour
{
    private GameObject _playerGameObject;
    private PlayerController _playerController;
    [SerializeField] EnemyWeaponGrenade _enemyWeaponGrenade;
    [SerializeField] Animator animator;
    [SerializeField] GameObject _droppableObjectPrefab;
    [SerializeField] GameObject _grenadePrefab;
    [SerializeField] LayerMask _player;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] bool _isRanged;
    [SerializeField] bool _isBigAnkleGrabber;
    [SerializeField] bool _isLittelAnkleGrabber;
    public Transform PlayerPosition;

    private int _maxHp = 5;
    public int _currentHp;
    public int Damage = 7;
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
                PlayerPosition = _playerGameObject.transform;
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
                ShootGrenade();
            }
            else
            {
                _enemyWeaponGrenade.EndShot();
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
    public void ShootGrenade()
    {
        isAttacking = true;
        _enemyWeaponGrenade.StartShot();
    }
    public void DrawLandingCircle(Vector3 targetPosition)
    {
        // Draw a red circle on the ground at the target position
        float radius = 0.1f; // Adjust the radius of the circle
        Vector3 circlePosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
        DebugDraw.Circle(circlePosition, Vector3.up, Color.red, radius);
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

            Vector3 dropPosition = new Vector3(transform.position.x + Random.Range(0.01f, 0.3f), -0.461f, transform.position.z + Random.Range(0.01f, 0.3f));
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
}
