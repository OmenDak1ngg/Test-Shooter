using System.Collections;
using UnityEngine;


public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;

    [SerializeField] private float _attackDelay;

    private WaitForSeconds _attackWait;
    private bool _canAttack;

    private void Awake()
    {
        _canAttack = true;
        _attackWait = new WaitForSeconds(_attackDelay);
    }

    public void DealDamage(Health health)
    {
        if (_canAttack == false)
            return;

        health.DecreaseAmount(_damage);
        _canAttack = false;

        StartCoroutine(StartAttackDelay());
    }

    private IEnumerator StartAttackDelay()
    {
        yield return _attackWait;

        _canAttack = true;
    }
}