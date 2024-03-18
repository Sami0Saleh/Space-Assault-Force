using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] PlayerController _playerController;
    [SerializeField] GameObject _bullet;

    [SerializeField] float _bulletRange;
    [SerializeField] float _fireRate;
    [SerializeField] float _reloatTime;
    [SerializeField] int _magazineSize;
    [SerializeField] bool _isAutomatic;
    private int _ammoLeft;

    public bool _isShooting;
    private bool _readyToShoot;
    public bool _reloading;

    private RaycastHit _rayHit;

    private void Awake()
    {
        _ammoLeft = _magazineSize;
        _readyToShoot = true;
    }
    
    void Update()
    {
        if (_isShooting && _readyToShoot && !_reloading && _ammoLeft > 0)
        {
            PerformShot();
        }
    }

    private void StartShot()
    {
        _isShooting = true;
    }
    private void EndShot()
    {
        _isShooting = false;
    }
    private void PerformShot()
    {
        _readyToShoot = false;
        Vector3 direction = _playerController.transform.forward;
        
        if (Physics.Raycast(transform.position, direction, out _rayHit, _bulletRange))
        {
            
            if (_rayHit.collider.gameObject.tag == "enemy")
            {
                Instantiate(_bullet, transform.position, Quaternion.LookRotation(direction));
            }
        }

        _ammoLeft--;
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
    public void Reload()
    {
        _reloading = true;
        Invoke("ReloadFinish", _reloatTime);
    }
    private void ReloadFinish()
    {
        _ammoLeft = _magazineSize;
        _reloading = false;
    }
}
