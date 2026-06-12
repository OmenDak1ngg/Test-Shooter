using System;
using System.Collections;
using UnityEngine;


public class Shooter : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;

    [SerializeField] private float _shootRange = 100f;
    [SerializeField] private Camera _camera;

    public event Action Hitted;

    private void OnEnable()
    {
        _userInput.ShootKeyPressed += Shoot;
    }

    private void OnDisable()
    {
        _userInput.ShootKeyPressed -= Shoot;
    }

    private void Shoot()
    {
        Vector3 center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = _camera.ScreenPointToRay(center);

        if (Physics.Raycast(ray, out RaycastHit hit, _shootRange))
        {
            if(hit.collider.TryGetComponent<Enemy>(out _))
            {
                Hitted?.Invoke();
            }
        }
    }
}