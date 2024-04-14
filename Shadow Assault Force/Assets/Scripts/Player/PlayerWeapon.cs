using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    [SerializeField] PlayerController _playerController;
    [SerializeField] Barrel _barrel;

    [SerializeField] float _bulletRange;
    public float FireRate;
    [SerializeField] bool _isAutomatic;
    private int _ammoLeft = 1;

    private bool _canShoot = false;
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
        Vector3 direction = _playerController.transform.forward;

        if (Physics.Raycast(transform.position, direction, out _rayHit, _bulletRange))
        {
            if (_rayHit.collider.gameObject.tag == "enemy")
            {
                _barrel.Shoot(direction);
            }
        }


        if (_ammoLeft >= 0)
        {
            Invoke("ResetShot", FireRate);

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
