using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _camTransform;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _assaultRifle;
    [SerializeField] PlayerWeapon _weapon;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LineRenderer _detectionRangeCircle;
    private int _maxHP = 20;
    public int CurrentHP;
    private GameObject _currentEnemy;
    [SerializeField] float _detectionRange;
    [SerializeField] bool currentEnemy;
    

    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed; 
    private Vector3 _moveDirection;
    private Vector3 originalForward;
    private Quaternion originalrotation;
    [SerializeField] bool _isMoving = false;

    private int _weaponIndex;
    public int Level = 0;
    private bool _isReloading = false;
    public static int EnemyCount = 1;

    public static bool IsplayerDead;

    private void Start()
    {
        IsplayerDead = false;
        originalForward = transform.forward;
        originalrotation = transform.rotation;
        CurrentHP = _maxHP;
        _weaponIndex = 0;
        StartCoroutine(SwitchWeapons());
    }
    void Update()
    {
        UpdateDetectionRangeCircle();
        Move();
        DetectEnemy();
        
        
    }
    public void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _moveDirection = (originalForward * verticalInput) + (transform.right * horizontalInput);


        // Move the player forward or backward
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime, Space.World);

        // Update camera position to follow the player
        if (_camTransform != null)
        {
            _camTransform.position = transform.position + new Vector3(0f, 1.25f, -1f);
        }
        if (_moveDirection != Vector3.zero)
        {
            _isMoving = true;
            if (_moveDirection == Vector3.forward)
            {
                _anim.SetBool("isFWD", true);
            }
            else
            {
                _anim.SetBool("isBWD", true);
            }
        }
        else
        {
            _isMoving = false;
            _anim.SetBool("isFWD", false);
            _anim.SetBool("isBWD", false);
        }
        if (_currentEnemy != null)
        {
            
            if (!_isMoving || !_isReloading)
            {
                _weapon._isShooting = true;
                _anim.SetBool("isShooting", true);
            }
            
        }
        else
        {
             transform.rotation = originalrotation;
            _weapon._isShooting = false;
            _anim.SetBool("isShooting", false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            _isReloading = true;
            _anim.SetBool("isReloading", true);
            _weapon.Reload();
        }
        else
        {
            _isReloading = false;
            _anim.SetBool("isReloading", false);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _weaponIndex = 0;
            _anim.SetBool("isPistol", true);
            _anim.SetBool("isAssaultRifle", false);
            StartCoroutine(SwitchWeapons());
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            _weaponIndex = 1;
            _anim.SetBool("isAssaultRifle", true);
            _anim.SetBool("isPistol", false);
            StartCoroutine(SwitchWeapons());
        }
    }
    public void UpdateDetectionRangeCircle()
    {
        // Calculate points for the detection range circle
        int pointCount = 50; // Number of points to define the circle
        _detectionRangeCircle.positionCount = pointCount;

        for (int i = 0; i < pointCount; i++)
        {
            float angle = (float)i / pointCount * 360f;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * _detectionRange;
            float z = Mathf.Cos(angle * Mathf.Deg2Rad) * _detectionRange;
            Vector3 point = transform.position + new Vector3(x, 0f, z);
            _detectionRangeCircle.SetPosition(i, point);
        }
    }
    public void DetectEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRange, _enemyLayer);

        if (hitColliders.Length > 0)
        {
            // Get the closest enemy
            float closestDistance = Mathf.Infinity;
            foreach (Collider col in hitColliders)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _currentEnemy = col.gameObject;
                    currentEnemy = true;
                    transform.LookAt(_currentEnemy.transform);
                }
                
            }
        }
        else
        {
            _currentEnemy = null;
            currentEnemy = false;
            
        }
    }
    public void MeleeTakeDamage(Grabber grabber)
    {
        CurrentHP -= grabber.Damage;
        if (CurrentHP <= 0)
        {
            IsplayerDead = true;
            Die();
        }
        grabber.FinishAttack();
    }
    public void RangeTakeDamage()
    {
        CurrentHP -= 2;
        if (CurrentHP <= 0)
        {
            IsplayerDead = true;
            Die();
        }
        
    }
    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabber"))
        {
            Grabber grabber = other.GetComponent<Grabber>();
            if (grabber != null)
            {
                MeleeTakeDamage(grabber);
            }
        }
    }

    IEnumerator SwitchWeapons()
    {

        yield return new WaitForSeconds(0.1f);


        if (_weaponIndex == 0)
        {
            _pistol.SetActive(true);
            _assaultRifle.SetActive(false);
        }
        if (_weaponIndex == 1)
        {
            _pistol.SetActive(false);
            _assaultRifle.SetActive(true);
        }
    }
}


