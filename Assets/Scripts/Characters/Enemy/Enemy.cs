using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyDamageDealer))]
public class Enemy : Character
{
    public EnemyMover Mover { get; private set; }

    private EnemyDamageDealer _damageDealer;

    private void OnEnable()
    {
        Mover.ReachedTarget += OnReachedTarget;
    }

    private void OnDisable()
    {
        Mover.ReachedTarget -= OnReachedTarget;
    }

    protected override void Awake()
    {
        base.Awake();

        _damageDealer = GetComponent<EnemyDamageDealer>();
        Mover = GetComponent<EnemyMover>(); 
    }

    private void OnReachedTarget(Transform targetTransform)
    {
        _damageDealer.DealDamage(targetTransform.GetComponent<Health>());
    }

    public void SetupOnCreate(Transform targetTransform)
    {
        Mover.SetTargetTransform(targetTransform);
        Mover.CalculateTresholdSize(targetTransform.localScale.x);
    }
}
