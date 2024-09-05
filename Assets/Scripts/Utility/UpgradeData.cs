using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeData : MonoBehaviour
{
    [SerializeField] private List<UpgradeLevels> _upgradeLevels = new List<UpgradeLevels>();

    private void Awake()
    {
        SendUpgradeData();
    }

    private void SendUpgradeData()
    {
        UpgradeManager upMan = UpgradeManager.Get();

        upMan.GetUpgradeLevelData(_upgradeLevels);
    }
}
