using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeSpawner : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;

    [SerializeField] Canvas UpgradeUI;
    [SerializeField] Button[] UpgradeButtons;

    private PlayerController _playerController;
    private LevelUIManager _levelUIManager;

    private int _numOfUpgradesToSpawn = 3;
    

    public void SetUpgradesInUI()
    {
        foreach (var button in UpgradeButtons)
        {
            button.image = upgradeItems
        }
    }

    public void OpenUpgradeUI()
    {
       UpgradeUI.enabled = true;
    }

    public void CloseUpgradeUI()
    {

    }

}
