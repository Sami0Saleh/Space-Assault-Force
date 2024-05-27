using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] CharacterController _charController;
    [SerializeField] LineRenderer _detectionRangeCircle;
    [SerializeField] float _detectionRange;
    [SerializeField] Vector3 _moveDirection;
    [SerializeField] bool _isMoving;
    [SerializeField] bool _isShooting;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] GameObject Bullet;
    [SerializeField] float _fireRate;

    private RaycastHit _rayHit;

    private void Update()
    {
        Move();
        DetectEnemy();
    }
    public void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Debug.Log(_moveDirection);
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
        }
        else
        {
            _isMoving = false;
        }
        Vector3 movement = _moveDirection * _moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.World);

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);
        if (inputDirection.magnitude > 0)
        {
            _isMoving = true;
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
    }

    public void DetectEnemy()
    {
        if (Physics.CheckSphere(transform.position, _detectionRange, _enemyLayer))
        {
            if (!_isShooting)
            {
                Invoke("Fire", _fireRate);
                _isShooting = true;
            }

        }
    }

    public void Fire()
    {
        Instantiate(Bullet);
        _isShooting = false;
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
    }
}