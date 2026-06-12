using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private readonly KeyCode ShowUpgradesKey = KeyCode.Escape;

    private readonly string Vertical = "Vertical";
    private readonly string Horizontal = "Horizontal";

    private readonly string MouseX = "Mouse X";
    private readonly string MouseY = "Mouse Y";

    private readonly KeyCode ShootKey = KeyCode.Mouse0;
    private readonly KeyCode SwitchKey = KeyCode.Q;

    private float _verticalDirection;
    private float _horizontalDirection;

    private float _mouseX;
    private float _mouseY;

    private bool _canControl;

    public event Action<Vector3> Moved;

    public event Action<float> MouseMovedX;
    public event Action<float> MouseMovedY;

    public event Action ClickedShowUpgrades;
    public event Action ShootKeyPressed;
    public event Action ShootKeyReleased;
    public event Action Switched;

    private void Awake()
    {
        _canControl = true;
        Cursor.lockState = CursorLockMode.Locked;
        //SaveSystem.DeleteSave();
        //SaveSystem.Load();
    }
    
    private void Update()
    {
        if(_canControl == false)
            return;
        
        _verticalDirection = Input.GetAxis(Vertical);  
        _horizontalDirection = Input.GetAxis(Horizontal);

        _mouseX = Input.GetAxis(MouseX);
        _mouseY = Input.GetAxis(MouseY);

        if(_verticalDirection != 0 || _horizontalDirection != 0)
            Moved?.Invoke(new Vector3(_horizontalDirection,0,_verticalDirection));

        if (_mouseX != 0)
            MouseMovedX?.Invoke(_mouseX);
    
        if (_mouseY != 0)
            MouseMovedY?.Invoke(_mouseY);
    
        if(Input.GetKey(ShootKey))
            ShootKeyPressed?.Invoke();  

        if(Input.GetKeyUp(ShootKey))
            ShootKeyReleased?.Invoke();

        if(Input.GetKeyDown(SwitchKey)) 
            Switched?.Invoke();

        if (Input.GetKeyDown(ShowUpgradesKey))
            ClickedShowUpgrades?.Invoke();
    }

    public void DeactivateControls()
    {
        _canControl = false;
    }
}
