using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _baseAmount;

    private int _amount;

    public event Action Killed;

    private void Awake()
    {
        _amount = _baseAmount;
    }

    public void DecreaseAmount(int decrease)
    {
        if(decrease < 0)
            throw new ArgumentOutOfRangeException(nameof(decrease));

        _amount -= decrease;

        if(_amount <= 0)
        {
            _amount = 0;
            Killed?.Invoke();
        }
    }

    public void ResetHealth()
    {
        _amount = _baseAmount;
    }
}