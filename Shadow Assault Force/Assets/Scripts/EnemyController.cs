using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject _droppableObjectPrefab;
    private int _maxHp = 5;
    private int _currentHp;

    private void Start()
    {
        _currentHp = _maxHp;
    }
    public void GotHit()
    {
        _currentHp--;
        Debug.Log("Got Hit By Bullet");
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        DropObjects();
        Destroy(gameObject);
    }

    void DropObjects()
    {
        // Randomize the number of objects dropped
        int numObjectsToDrop = Random.Range(1, 5); // Example: Drop between 1 to 4 objects

        for (int i = 0; i < numObjectsToDrop; i++)
        {
            // Instantiate the droppable object with random position offset
            Vector3 dropPosition = transform.position + Random.insideUnitSphere * 2f; // Example: Drop objects within a 2-unit radius
            Instantiate(_droppableObjectPrefab, dropPosition, Quaternion.identity);
        }
    }


}
