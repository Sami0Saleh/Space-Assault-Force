using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponGrenade : MonoBehaviour
{
    [SerializeField] EnemyGrenadeController _enemyGrenadeController;
    [SerializeField] GameObject _grenade;

    [SerializeField] float _grenadeRange;
    [SerializeField] float _fireRate;
    [SerializeField] bool _isAutomatic;
    private int _ammoLeft = 1;

    public bool _canShoot;
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
        Vector3 targetPosition = _enemyGrenadeController.PlayerPosition.transform.position;
        if (Physics.Raycast(transform.position, targetPosition, out _rayHit, _grenadeRange))
        {

            if (_rayHit.collider.gameObject.tag == "Player")
            {
                // Draw a red circle on the ground where the grenade will land
                _enemyGrenadeController.DrawLandingCircle(targetPosition);

                GameObject grenade = Instantiate(_grenade, transform.position, Quaternion.identity);
                // Initiate the grenade's arc trajectory towards the target position
                grenade.GetComponent<Grenade>().Shoot(targetPosition);
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
