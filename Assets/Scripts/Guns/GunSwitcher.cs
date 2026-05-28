using System;
using System.Collections;
using UnityEngine;


public class GunSwitcher : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;

    [SerializeField] private UserInput _userInput;

    private Gun _currentGun;

    public Transform CurrentTransformShootPoint { get; private set; }

    public event Action<Transform> GunSwitched;

    private void OnEnable()
    {
        _userInput.Switched += SwitchGun;
    }

    private void OnDisable()
    {
        _userInput.Switched -= SwitchGun;
    }

    private void Awake()
    {
        _currentGun = _guns[0];
        CurrentTransformShootPoint = _currentGun.ShootPoint;
        _guns[0].gameObject.SetActive(true);
        _guns[1].gameObject.SetActive(false);
    }

    private void SwitchGun()
    {
        _currentGun.gameObject.SetActive(false);

        int indexOfNextGun = _currentGun == _guns[0] ? 1 : 0;

        _currentGun = _guns[indexOfNextGun];
        _currentGun.gameObject.SetActive(true);

        GunSwitched?.Invoke(_currentGun.ShootPoint);
    }
}