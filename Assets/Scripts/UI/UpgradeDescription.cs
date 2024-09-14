using System;
using TMPro;
using UnityEngine;

public class UpgradeDescription : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _resource;
    
    public void UpdateDescription(string name, string description, ResourceType type)
    {
        _name.text = name;
        _description.text = description;
        _resource.text = Enum.GetName(typeof(ResourceType), type);
    }
}
