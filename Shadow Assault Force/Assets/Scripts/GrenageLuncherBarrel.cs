using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenageLuncherBarrel : MonoBehaviour
{
    [SerializeField] GameObject _grenade;

    public void Shoot(Vector3 targetPosition)
    {
        GameObject grenade = Instantiate(_grenade, transform.position, Quaternion.identity);
        if (grenade != null && targetPosition != null)
            grenade.GetComponent<Grenade>().Launch(targetPosition);
    }
}
