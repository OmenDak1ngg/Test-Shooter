using UnityEngine;

public class AgilityUpgraderByDistance : MonoBehaviour
{
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private PlayerMover _playerMover;

    [SerializeField] private float _distanceForExperience = 5f;

    private Transform _playerTransform;
    private Vector3 _lastPosition;
    private float _distanceAccumulator;

    private void Start()
    {
        _playerTransform = _playerMover.transform;

        _lastPosition = _playerTransform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(_playerTransform.position, _lastPosition);

        _distanceAccumulator += distance;

        while (_distanceAccumulator >= _distanceForExperience)
        {
            _distanceAccumulator -= _distanceForExperience;

            _attributes.AddExperience(AttributeType.Agility);
            _attributes.TryUpgrade(AttributeType.Agility);
        }

        _lastPosition = _playerTransform.position;
    }
}