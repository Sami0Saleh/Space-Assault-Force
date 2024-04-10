using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgradeItem", menuName = "Upgrade Item")]
public class UpgradeItem : ScriptableObject
{
    [SerializeField] Sprite[] UpgradeSpriteList;
    [SerializeField] Sprite UpgradeSprite;
    public string itemName;
    public string description;
    public UpgradeType upgradeType;
    public GameObject prefab;
    public float value;
    private PlayerController _playerController;
    private LevelUIManager _levelUIManager;

    public void ApplyUpgrade()
    {
        
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                _playerController.IncreaseDamage((int)value);
                _levelUIManager.CloseUpgrades();
                UpgradeSprite = UpgradeSpriteList[1];

                break;
            case UpgradeType.Health:
                _playerController.IncreaseHealth((int)value);
                _levelUIManager.CloseUpgrades();
                UpgradeSprite = UpgradeSpriteList[2];
                break;
            case UpgradeType.FireRate:
                _playerController.UpdateFireRate(value);
                _levelUIManager.CloseUpgrades();
                UpgradeSprite = UpgradeSpriteList[3];
                break;
            default:
                Debug.LogWarning("Unknown upgrade type: " + upgradeType);
                break;
        }
    }
    public void SetPlayer(PlayerController player)
    {
        _playerController = player;
    }
    public void SetUIManager(LevelUIManager LevelUIManager)
    {
        _levelUIManager = LevelUIManager;
    }
}
public enum UpgradeType
{
    Damage,
    Health,
    FireRate
}