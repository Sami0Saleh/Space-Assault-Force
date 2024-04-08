using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeItem", menuName = "Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public string itemName;
    public string description;
    public UpgradeType upgradeType;
    public GameObject prefab;
    public int value;

    public void ApplyUpgrade(PlayerController player)
    {
        player = player.GetComponent<PlayerController>();
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                player.IncreaseDamage(value);
                break;
            case UpgradeType.Health:
                player.IncreaseHealth(value);
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }
}
public enum UpgradeType
{
    Damage,
    Health
}