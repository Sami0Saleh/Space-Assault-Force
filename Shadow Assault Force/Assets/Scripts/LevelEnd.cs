using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Level++;
                PlayerController.EnemyCount = 1;
                _levelManager.StartLevel();
            }
            
        }
    }
}
