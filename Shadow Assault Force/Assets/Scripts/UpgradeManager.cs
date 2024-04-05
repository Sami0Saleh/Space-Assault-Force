using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;
    public int numberOfUpgradesToSpawn = 3;
    public float spawnRadius = 5f;

    public void SpawnRandomUpgrades(Vector3 playerPosition)
    {
        for (int i = 0; i < numberOfUpgradesToSpawn; i++)
        {
            Vector3 randomPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
            int randomIndex = Random.Range(0, upgradeItems.Length);
            GameObject upgradePrefab = Instantiate(upgradeItems[randomIndex].prefab, randomPosition, Quaternion.identity);
        }
    }
}
