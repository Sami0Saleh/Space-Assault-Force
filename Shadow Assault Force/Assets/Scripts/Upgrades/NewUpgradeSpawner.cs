using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewUpgradeSpawner : MonoBehaviour
{
    [SerializeField] Canvas UpgradeUI;

    [SerializeField] UpgradeItem[] UpgradesSO; 
    [SerializeField] Button[] newUpgradeButtons;
    [SerializeField] Button[] oldUpgradeButtons;

    [SerializeField] PlayerController _playerController;
    [SerializeField] LevelUIManager _levelUIManager;

    private int _numOfUpgradesToSpawn = 3;


    public void SetUpgradesInUI()
    {
        
        foreach (var button in newUpgradeButtons)
        {
            int index = Random.Range(0, oldUpgradeButtons.Length);
            SetUpgrade(index);
            button.image.sprite = UpgradesSO[index].UpgradeSprite;
            button.onClick.RemoveAllListeners();
            button.onClick = oldUpgradeButtons[index].onClick;
            button.onClick.AddListener(CloseUpgradeUI);
        }

    }

    public void SetUpgrade(int index)
    {
        UpgradesSO[index].SetPlayer(_playerController);
        UpgradesSO[index].SetUIManager(_levelUIManager);
    }
   
    public void OpenUpgradeUI()
    {
        SetUpgradesInUI();
        UpgradeUI.gameObject.SetActive(true);
        Time.timeScale = 0;
  
    }

    public void CloseUpgradeUI()
    {
        UpgradeUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SetUpgradeOnButton()
    {


    }


}
