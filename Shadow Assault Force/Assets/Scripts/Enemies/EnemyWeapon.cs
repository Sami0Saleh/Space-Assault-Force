using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] EnemyShooterController _enemyShooterController;
    [SerializeField] Barrel _barrel;

    [SerializeField] float _bulletRange;
    [SerializeField] float _fireRate;
    [SerializeField] bool _isAutomatic;
    private int _ammoLeft = 1;

    private bool _canShoot;
    private bool _readyToShoot;

    private RaycastHit _rayHit;

    private void Awake()
    {
        _readyToShoot = true;
    }

    void Update()
    {
        if (_canShoot && _readyToShoot)
        {
            PerformShot();
        }
    }

    public void StartShot()
    {
        _canShoot = true;
    }
    public void EndShot()
    {
        _canShoot = false;
    }
    private void PerformShot()
    {
        _readyToShoot = false;
        Vector3 direction = _enemyShooterController.transform.forward;

        if (Physics.Raycast(transform.position, direction, out _rayHit, _bulletRange))
        {

            if (_rayHit.collider.gameObject.tag == "Player")
            {
                _barrel.Shoot(direction);
            }
        }

       
        if (_ammoLeft >= 0)
        {
            Invoke("ResetShot", _fireRate);

            if (!_isAutomatic)
            {
                EndShot();
            }
        }
    }
    private void ResetShot()
    {
        _readyToShoot = true;
    }
    
}
