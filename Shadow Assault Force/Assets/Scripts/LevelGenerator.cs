using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int numberOfObstacles = 20;
    [SerializeField] int numberOfEnemies = 10;
    public Vector3 levelSize = new Vector3(50, 0, 50); // Width and height of the level

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-levelSize.x / 2, levelSize.x / 2), Random.Range(-levelSize.y / 2, levelSize.y / 2), Random.Range(-levelSize.z / 2, levelSize.z / 2));
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], spawnPosition, Quaternion.identity);
        }

        // Generate enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-levelSize.x / 2, levelSize.x / 2), Random.Range(-levelSize.y / 2, levelSize.y / 2), Random.Range(-levelSize.z / 2, levelSize.z / 2));
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, Quaternion.identity);
        }
    }
}
