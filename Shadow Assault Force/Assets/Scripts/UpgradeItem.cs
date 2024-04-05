using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeItem", menuName = "Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public UpgradeType upgradeType;
    public GameObject prefab;
    public int value;

    public void ApplyUpgrade(PlayerController player)
    {
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