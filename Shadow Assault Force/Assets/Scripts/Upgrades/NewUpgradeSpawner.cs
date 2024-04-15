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

    private int _index1 = -1;
    private int _index2 = -1;

    public void SetUpgradesInUI()
    {
        int index = Random.Range(0, oldUpgradeButtons.Length);
        _index1 = index;
        foreach (var button in newUpgradeButtons)
        {
            SetUpgrade(index);
            button.image.sprite = UpgradesSO[index].UpgradeSprite;
            button.onClick.RemoveAllListeners();
            button.onClick = oldUpgradeButtons[index].onClick;
            button.onClick.AddListener(CloseUpgradeUI);
            index = Random.Range(0, oldUpgradeButtons.Length);
            while (index == _index1 || index == _index2)
            {
                index = Random.Range(0, oldUpgradeButtons.Length);
            }
            _index2 = index;
        }

    }

    public void SetUpgrade(int index)
    {
        UpgradesSO[index].SetPlayer(_playerController);
        UpgradesSO[index].SetUIManager(this);
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
