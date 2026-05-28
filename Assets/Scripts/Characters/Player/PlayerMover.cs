using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravityForce = 9.8f;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _horizontalSensitivity;

    private CharacterController _characterController;
    private float _currentGravityForce;
    private Transform _cameraTransform;

    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Vector3 _relativeMove;

    private void OnEnable()
    {
        _userInput.Moved += Move;
        _userInput.MouseMovedX += RotateX;
    }

    private void OnDisable()
    {
        _userInput.Moved -= Move;
        _userInput.MouseMovedX -= RotateX;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _currentGravityForce = _gravityForce;
        _cameraTransform = _camera.transform;
    }

    private void Update()
    {
        HandleGravity();
    }

    private void Move(Vector3 directoin)
    {
        _cameraForward = _cameraTransform.forward;
        _cameraRight = _cameraTransform.right;

        _cameraForward.y = 0;
        _cameraRight.y = 0;

        _relativeMove = _cameraForward.normalized * directoin.z + _cameraRight.normalized * directoin.x;
        _relativeMove.y = _currentGravityForce;

        _characterController.Move(_relativeMove * _moveSpeed * Time.deltaTime);
    }

    private void RotateX(float rotationX)
    {
        transform.Rotate(Vector3.up * rotationX * _horizontalSensitivity);
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded == false)
        {
            _currentGravityForce -= _gravityForce * Time.deltaTime;
        }
        else
        {
            _currentGravityForce = 0f;
        }
    }
}