using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private string _upgradeName;
    [SerializeField] private string _upgradeDescription;
    [SerializeField] private UpgradeDescription _description;
    private int _upgradeLevel;
    private int _resourcesNeeded;
    private int _resourcesObtained;

    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _resources;

    public void OnSelect(BaseEventData eventData)
    {
        _description.UpdateDescription(_upgradeName, _upgradeDescription, _resourceType);
    }

    private void OnEnable()
    {
        UpgradeManager upgradeManager = UpgradeManager.Get();
        ResourceManager resourceManager = ResourceManager.Get();

        _upgradeLevel = upgradeManager.GetUpgradeLevel(_resourceType);
        _resourcesNeeded = upgradeManager.GetUpgradePrice(_resourceType);
        _resourcesObtained = (int)resourceManager.GetResourceAmount(_resourceType);

        _level.text = _upgradeLevel.ToString();
        _resources.text = _resourcesObtained.ToString() + "/" + _resourcesNeeded.ToString();
    }
}
