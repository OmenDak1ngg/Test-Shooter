using UnityEngine;

public class ModelUpgradeHandler : MonoBehaviour
{
    [SerializeField] private ModelChanger _modelChanger;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private AttributeType _attributeType = AttributeType.Strength;

    private void OnEnable()
    {
        _attributes.AttributeChanged += TryChangeModel;
    }

    private void OnDisable()
    {
        _attributes.AttributeChanged -= TryChangeModel;
    }

    private void Start()
    {
        TryChangeModel(_attributeType);
    }

    private void TryChangeModel(AttributeType type)
    {
        if (type != _attributeType)
            return;

        _modelChanger.UpgradeByLevel(_attributes.GetLevel(_attributeType));
    }
}