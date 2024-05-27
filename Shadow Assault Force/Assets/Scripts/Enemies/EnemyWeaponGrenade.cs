using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponGrenade : MonoBehaviour
{
    [SerializeField] EnemyGrenadeController _enemyGrenadeController;
    [SerializeField] GrenageLuncherBarrel _grenageLuncherBarrel;

    [SerializeField] float _grenadeRange;
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
        // Calculate target position (e.g., player's position)
        Vector3 direction = _enemyGrenadeController.transform.forward;
        if (Physics.Raycast(transform.position, direction, out _rayHit, _grenadeRange))
        {

            if (_rayHit.collider.gameObject.tag == "Player")
            {
                Debug.Log(_rayHit.collider.gameObject.tag);
                // Draw a red circle on the ground where the grenade will land
                //_enemyGrenadeController.DrawLandingCircle(_enemyGrenadeController._payloadTransform.transform.position);
                _grenageLuncherBarrel.Shoot(_enemyGrenadeController._payloadTransform.position);
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
