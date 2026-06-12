using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private readonly float HalfDivider = 2f;

    [SerializeField] private Transform _shootPoint;

    [SerializeField] protected UserInput UserInput;

    [SerializeField] private float _shootLength = 50;
    [SerializeField] private int _damage;

    public Transform ShootPoint => _shootPoint;

    protected bool CanShoot;

    public event Action Hitted;
    public event Action Shooted;

    protected virtual void OnEnable()
    {
        UserInput.ShootKeyPressed += Shoot;
    }

    protected virtual void OnDisable()
    {
        UserInput.ShootKeyPressed -= Shoot;
    }

    protected virtual void Awake()
    {
        CanShoot = true;
    }

    protected virtual void Shoot()
    {
        if (CanShoot == false)
            return;

        Vector3 shootCenter = new Vector3(Screen.width / HalfDivider, Screen.height / HalfDivider, 0);
        Ray ray = Camera.main.ScreenPointToRay(shootCenter);
        RaycastHit hit;

        Shooted?.Invoke();

        if (Physics.Raycast(ray, out hit, _shootLength))
        {
            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Health.DecreaseAmount(_damage);
                Hitted?.Invoke();
            }
        }
    }
}
