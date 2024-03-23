using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _camTransform;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _pistol;
    [SerializeField] GameObject _assaultRifle;
    [SerializeField] Weapon _weapon;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LineRenderer _detectionRangeCircle;

    private GameObject _currentEnemy;
    [SerializeField] float _detectionRange;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed; 
    private Vector3 _moveDirection;
    [SerializeField] bool _isMoving = false;

    private int _weaponIndex;
    public int Level = 0;

    private void Start()
    {
        _weaponIndex = 0;
        StartCoroutine(SwitchWeapons());
    }
    void Update()
    {
        UpdateDetectionRangeCircle();
        Move();
        DetectEnemy();
        if (_currentEnemy != null)
        {
            transform.LookAt(_currentEnemy.transform);
            if (!_isMoving )
            {
                _weapon._isShooting = true;
                _anim.SetBool("isShooting", true);
            }
        }
        else
        {
            _weapon._isShooting = false;
            _anim.SetBool("isShooting", false);
        }
    }
    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        

        // Rotate the player left or right
        transform.Rotate(Vector3.up, horizontalInput * _rotationSpeed * Time.deltaTime);

        // Move the player forward or backward
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);

        // Update camera position to follow the player
        if (_camTransform != null)
        {
            _camTransform.position = transform.position + new Vector3(0f, 1f, -1f);
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
        /*if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            _anim.SetBool("isFWD", true); 
            _anim.SetBool("isBWD", false); 
            _anim.SetBool("isLeft", false);
            _anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _speed * Time.deltaTime);
            _anim.SetBool("isBWD", true); 
            _anim.SetBool("isFWD", false);
            _anim.SetBool("isLeft", false);
            _anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _anim.SetBool("isFWD", false);
            _anim.SetBool("isBWD", false);
            _anim.SetBool("isLeft", false);
            _anim.SetBool("isRight", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _anim.SetBool("isFWD", false);
            _anim.SetBool("isBWD", false);
            _anim.SetBool("isLeft", true);
            _anim.SetBool("isRight", false);
        }*/
       
        if (Input.GetKey(KeyCode.R))
        {
            _anim.SetBool("isReloading", true);
            _weapon.Reload();
        }
        else
        {
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
                    
                }
                
            }
        }
        else
        {
            _currentEnemy = null;
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


