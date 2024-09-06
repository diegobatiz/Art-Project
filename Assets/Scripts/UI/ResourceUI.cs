using System;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceType _type;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;

    private string _itemName;
    private uint _itemAmount;

    private void Awake()
    {
        _itemName = Enum.GetName(typeof(ResourceType), _type);

        _name.text = _itemName;
    }

    private void OnEnable()
    {
        _itemAmount = ResourceManager.Get().GetResourceAmount(_type);

        _amount.text = _itemAmount.ToString();
    }
}
