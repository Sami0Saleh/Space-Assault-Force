using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;
    public int numberOfUpgradesToSpawn = 3;
    public float spawnRadius = 5f;
    
    


    public void SpawnRandomUpgrades()
    {
        float offsetX = 0.2f;
        for (int i = 0; i < numberOfUpgradesToSpawn; i++)
        {
            if (i == 0)
            {
                Vector3 randomPosition = new Vector3(0f, 0f, 0f);
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
            }
            else if (i == 1)
            {
                float randomX = 0f - offsetX;
                Vector3 randomPosition = new Vector3(randomX, 0f, 0f);
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
            }
            else if (i == 2)
            {
                float randomX = 0f + offsetX;
                Vector3 randomPosition = new Vector3(randomX, 0f, 0f);
                //Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
                int randomIndex = Random.Range(0, upgradeItems.Length);
                GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
            }

        }
    }
}
