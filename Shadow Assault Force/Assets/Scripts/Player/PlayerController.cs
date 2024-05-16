using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Vector3 CamPosition;
    [SerializeField] List<IEnemy> _enemies = new List<IEnemy>();
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _camTransform;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _assaultRifle;
    [SerializeField] PlayerWeapon _weapon;
    [SerializeField] LevelUIManager _levelUIManager;
    [SerializeField] FloatingJoystick _joystick;
    //[SerializeField] UpgradeSpawner _upgradeSpawner;
    [SerializeField] NewUpgradeSpawner _newUpgradeSpawner;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LineRenderer _detectionRangeCircle;
    [SerializeField] float _detectionRange;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] bool _isMoving = false;

    private GameObject _currentEnemy;
    public Coins Coin;

    private int _maxHP = 1000;
    public int CurrentHP;
    public int Damage = 2;
    private Vector3 _moveDirection;
    private Quaternion originalrotation;
    public int LevelCoins = 0;
    public int PlayerLevel = 1;
    public int PlayerLevelXP = 0;
    public int PlayerLevelMaxXP;
    private int _weaponIndex;
    public int Level = 0;
    public static int EnemyCount = 3;
    public static bool IsplayerDead;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        CurrentHP = _maxHP;
        PlayerLevelMaxXP = 25;
        _levelUIManager.UpdatePlayerHP(CurrentHP, _maxHP);
        _levelUIManager.UpdatePlayerLevel(PlayerLevel);
        _levelUIManager.UpdatePlayerXP(PlayerLevelXP, PlayerLevelMaxXP);
        _levelUIManager.UpdatePlayerCoins(LevelCoins);
    }
    private void Start()
    {
        IsplayerDead = false;
        originalrotation = transform.rotation;
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

        /* float horizontalInput = Input.GetAxis("Horizontal");
         float verticalInput = Input.GetAxis("Vertical");

         _moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

         if (_moveDirection.magnitude > 0)
         {
             _moveDirection.Normalize();
         }
         if (_moveDirection != Vector3.zero)
         {
             _isMoving = true;
             Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
             _anim.SetBool("isFWD", true);
         }
         else
         {
             _isMoving = false;
             _anim.SetBool("isFWD", false);
         }
         Vector3 movement = _moveDirection * _moveSpeed * Time.deltaTime;
         transform.Translate(movement, Space.World);*/

        Vector3 inputDirection = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
        if (inputDirection.magnitude > 0)
        {
            _isMoving = true;
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            _anim.SetBool("isFWD", true);
        }
        else
        {
            _isMoving = false;
            _anim.SetBool("isFWD", false);
        }

        Vector3 movement = inputDirection * _moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        // Update camera position to follow the player
        if (_camTransform != null)
        {
            _camTransform.position = transform.position + CamPosition;
        }
        if (_currentEnemy != null)
        {
            if (!_isMoving)
            {
                transform.LookAt(_currentEnemy.transform);
                _weapon.StartShot();
                _anim.SetBool("isShooting", true);
            }
            else
            {
                _weapon.EndShot();
                _anim.SetBool("isShooting", false);
            }
        }
        else if (_currentEnemy == null)
        {
            if (_isMoving)
            {
                _weapon.EndShot();
                _anim.SetBool("isShooting", false);
            }
            else
            {
                transform.rotation = originalrotation;
                _weapon.EndShot();
                _anim.SetBool("isShooting", false);
            }
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

        // Clear the detectedEnemies list before populating it with new detections
        _enemies.Clear();

        if (hitColliders.Length > 0)
        {
            // Loop through all detected enemy colliders
            float closestDistance = Mathf.Infinity;
            foreach (Collider col in hitColliders)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    IEnemy enemyController = col.GetComponent<IEnemy>();
                    _currentEnemy = col.gameObject;
                    if (enemyController != null)
                    {
                        _enemies.Add(enemyController);
                    }
                   
                }
            }
        }
        else
        {
            _currentEnemy = null;
        }
    }
    public void TakeDamageFromEnemies()
    {
        // Loop through all detected enemies and apply damage
        foreach (IEnemy enemy in _enemies)
        {
            if (enemy != null)
            {
                if (enemy is EnemyShooterController)
                {
                    TakeRangeDamage((enemy as EnemyShooterController).Damage);
                }
                else if (enemy is EnemyGrenadeController)
                {
                    TakeRangeDamage((enemy as EnemyGrenadeController).Damage);
                }
                // Add more enemy types if needed
            }
        }
    }
    public void TakeMeleeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            IsplayerDead = true;
            Die();
        }
        _levelUIManager.UpdatePlayerHP(CurrentHP, _maxHP);
    }
    public void TakeRangeDamage(int damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            IsplayerDead = true;
            Die();
        }
        _levelUIManager.UpdatePlayerHP(CurrentHP, _maxHP);
    }
    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    public void UpdatePlayerCoins()
    {
        Meta.Coins += LevelCoins;
    }
    public void UpdatePlayerLevel()
    {
        if (PlayerLevelXP >= PlayerLevelMaxXP)
        {
            Debug.Log("Should Be Able to Level Up");
            PlayerLevel++;
            PlayerLevelXP -= PlayerLevelMaxXP;
            PlayerLevelMaxXP += 25;
            //_newUpgradeSpawner.OpenUpgrades();
            // _upgradeSpawner.SpawnRandomUpgrades();
            _newUpgradeSpawner.OpenUpgradeUI();
        }
        _levelUIManager.UpdatePlayerLevel(PlayerLevel);
        _levelUIManager.UpdatePlayerXP(PlayerLevelXP, PlayerLevelMaxXP);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Coin = other.GetComponent<Coins>();
            LevelCoins++;
            PlayerLevelXP++;
            _levelUIManager.UpdatePlayerCoins(LevelCoins);
            UpdatePlayerLevel();
        }
        if (other.CompareTag("bullet") || other.CompareTag("grenade"))
        {
            TakeDamageFromEnemies();
        }
    }
    public void IncreaseDamage(int value)
    {
        Damage += value;
    }
    public void IncreaseHealth(int value)
    {
        _maxHP += value;
        CurrentHP += value;
        _levelUIManager.UpdatePlayerHP(CurrentHP, _maxHP);
    }
    public void UpdateFireRate(float value)
    {
        _weapon.FireRate -= value;
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


