using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;
    public int numberOfUpgradesToSpawn = 3;
    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelUIManager _levelUIManager;
    private int _index1 = -1;
    private int _index2 = -1;
    public void SpawnRandomUpgrades()
    {
        float offsetX = 200f;
        int i = 0;
        int randomIndex = Random.Range(0, upgradeItems.Length);
        while (i < numberOfUpgradesToSpawn)
        {
            if (i == 0)
            {
                Vector3 randomPosition = transform.position;
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    //upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
                _index1 = randomIndex;
            }
            else if (i == 1)
            {
                
                float randomX = transform.position.x - offsetX;
                Vector3 randomPosition = new Vector3(randomX, transform.position.y, transform.position.z);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    //upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
                _index2 = randomIndex;
            }
            else if (i == 2)
            {
                float randomX = transform.position.x + offsetX;
                Vector3 randomPosition = new Vector3(randomX, transform.position.y, transform.position.z);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
                upgradePrefab.transform.SetParent(transform, true);
                if (_playerController != null)
                {
                    upgradeItems[randomIndex].SetPlayer(_playerController);
                    //upgradeItems[randomIndex].SetUIManager(_levelUIManager);
                }
            }
            i++;
            randomIndex = Random.Range(0, upgradeItems.Length);
            while (randomIndex == _index1 || randomIndex == _index2)
            {
                randomIndex = Random.Range(0, upgradeItems.Length);
            }
        }
    }
}
