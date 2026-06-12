using UnityEngine;

public class MoverUpgradeHandler : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private AttributeType _attributeType = AttributeType.Agility;

    private void OnEnable()
    {
        _attributes.AttributeChanged += UpdateSpeed;
    }

    private void OnDisable()
    {
        _attributes.AttributeChanged -= UpdateSpeed;
    }

    private void Start()
    {
        UpdateSpeed(_attributeType);
    }

    private void UpdateSpeed(AttributeType type)
    {
        if (type != _attributeType)
            return;

        _mover.UpgradeByLevel(_attributes.GetLevel(_attributeType));
    }
}