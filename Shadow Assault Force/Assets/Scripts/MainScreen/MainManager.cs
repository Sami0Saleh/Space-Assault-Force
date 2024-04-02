using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;
    public static int Energy = 20;
    private int MaxEnergy = 20;

    private void Start()
    {
        EnergyUpdate();
    }


    public void EnergyUpdate()
    {
        _uiManager.UpdateEnergyText();
    }
}
