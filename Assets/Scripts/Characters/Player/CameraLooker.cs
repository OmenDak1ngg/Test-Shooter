using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraLooker : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;

    [SerializeField] private float _minY = -90f;
    [SerializeField] private float _maxY = 90f;

    [SerializeField] private float _verticalSensitivity;

    private Camera _camera;
    private float _rotationY;

    private void OnEnable()
    {
        _userInput.MouseMovedY += RotateY;
    }

    private void OnDisable()
    {
        _userInput.MouseMovedY -= RotateY;    
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _rotationY = 0f;
    }

    private void RotateY(float mouseY)
    {
        _rotationY -= mouseY * _verticalSensitivity;
        _rotationY = Mathf.Clamp(_rotationY,_minY, _maxY);

        _camera.transform.localRotation = Quaternion.Euler(_rotationY,0,0);
    }

}