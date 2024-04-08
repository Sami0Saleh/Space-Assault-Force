using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;
    public int numberOfUpgradesToSpawn = 3;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelUIManager _levelUIManager;

    public void SpawnRandomUpgrades()
    {
        float offsetX = 200f;
        for (int i = 0; i < numberOfUpgradesToSpawn; i++)
        {
            if (i == 0)
            {
                Vector3 randomPosition = transform.position;
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
            }
            else if (i == 1)
            {
                float randomX = transform.position.x - offsetX;
                Vector3 randomPosition = new Vector3(randomX, transform.position.y, transform.position.z);
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
            }
            else if (i == 2)
            {
                float randomX = transform.position.x + offsetX;
                Vector3 randomPosition = new Vector3(randomX, transform.position.y, transform.position.z);
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
            }
            
        }
    }
}
