using UnityEngine;

public class UpgradeModelHandler : MonoBehaviour
{
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private AttributeType _attributeType;
    [SerializeField] private Gun _gun;

    private void OnEnable()
    {
        _gun.Hitted += AddValueToSystem;
    }

    private void OnDisable()
    {
        _gun.Hitted -= AddValueToSystem;
    }

    private void AddValueToSystem()
    {
        _attributes.AddExperience(_attributeType);
        _attributes.TryUpgrade(_attributeType);
    }
}