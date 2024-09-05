using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradeLevels
{
    public ResourceType resourceType;
    public int levelAmount;
    public int[] levelPrices;
}

public class UpgradeManager
{
    private static UpgradeManager _instance;

    public static UpgradeManager Get()
    {
        if (_instance == null)
        {
            _instance = new UpgradeManager();
        }

        return _instance;
    }

    private Dictionary<ResourceType, int> _currentUpgradeLevel;
    private Dictionary<ResourceType, UpgradeLevels> _upgradeLevels;
    private bool _hasData = false;

    public void LoadUpgrades()
    {
        if (_hasData)
        {
            return;
        }

        _hasData = true;
    }

    public void SaveUpgrades()
    {

    }

    public void GetUpgradeLevelData(List<UpgradeLevels> data)
    {
        if  (_hasData)
        {
            return;
        }

        foreach (UpgradeLevels level in data)
        {
            _currentUpgradeLevel.Add(level.resourceType, 1);
            _upgradeLevels.Add(level.resourceType, level);
        }

        _hasData = true;
    }

    public int GetUpgradeLevel(ResourceType type)
    {
        return _currentUpgradeLevel[type];
    }

    public int GetUpgradePrice(ResourceType type)
    {
        UpgradeLevels level = _upgradeLevels[type];
        int currentLevel = _currentUpgradeLevel[type];

        if (currentLevel == level.levelAmount)
        {
            return -1;
        }

        return level.levelPrices[currentLevel + 1];
    }

    public void Upgrade(ResourceType type)
    {

    }
}
