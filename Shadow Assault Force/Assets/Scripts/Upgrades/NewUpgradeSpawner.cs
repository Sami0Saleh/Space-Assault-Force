using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeSpawner : MonoBehaviour
{
    public UpgradeItem[] upgradeItems;

    [SerializeField] Canvas UpgradeUI;
    [SerializeField] Button[] UpgradeButtons;

    //[SerializeField] Sprite 
    private PlayerController _playerController;
    private LevelUIManager _levelUIManager;

    private int _numOfUpgradesToSpawn = 3;


    public void SetUpgradesInUI()
    {
        int index = Random.Range(0, upgradeItems.Length);
        foreach (var button in UpgradeButtons)
        {
            button.image.sprite = upgradeItems[index].UpgradeSprite;
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
