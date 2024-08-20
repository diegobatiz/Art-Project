using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    HorseHide,
    BullHorn,
    BirdFeather,
    RhinoHorn
}

public class ResourceManager
{
    private static ResourceManager _instance;

    private Dictionary<ResourceType, uint> _resources;

    //Singleton Functions
    private ResourceManager()
    {
        _resources.Add(ResourceType.HorseHide, 0);
        _resources.Add(ResourceType.BullHorn, 0);
        _resources.Add(ResourceType.BirdFeather, 0);
        _resources.Add(ResourceType.RhinoHorn, 0);
    }

    public static ResourceManager Get()
    {
        if (_instance == null)
        {
            _instance = new ResourceManager();
        }

        return _instance;
    }


    //Manager Functions
    public void AddResource(ResourceType type, uint amount)
    {
        _resources[type] += amount;
    }

    public bool UseResource(ResourceType type, uint amount)
    {
        if (_resources[type] - amount < 0)
        {
            return false;
        }

        _resources[type] -= amount;
        return true;
    }

    public void SaveResources()
    {

    }

    public void LoadResources()
    {

    }
}
