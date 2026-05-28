using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _tresholdOffset = 1f;

    private NavMeshAgent _agent;
    private Transform _targetTransform;
    private Coroutine _moveCoroutine;

    private float _treshold;

    public event Action<Transform> ReachedTarget;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
    }

    private void OnDisable()
    {
        StopCoroutine(_moveCoroutine);
    }

    private IEnumerator MoveToTarget()
    {
        while (enabled)
        {
            _agent.SetDestination(_targetTransform.position);

            if (_agent.pathPending)
            {
                yield return null;
            }
            else
            {
                if (_agent.remainingDistance <= _treshold)
                    ReachedTarget?.Invoke(_targetTransform);

                yield return null;
            }
        }
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    public void StartMoveToTarget()
    {
        _moveCoroutine = StartCoroutine(MoveToTarget());
    }

    public void CalculateTresholdSize(float targetSize)
    {
        _treshold = GetComponent<CapsuleCollider>().radius * transform.localScale.x;

        _treshold += targetSize;
        _treshold += _tresholdOffset;
    }
}