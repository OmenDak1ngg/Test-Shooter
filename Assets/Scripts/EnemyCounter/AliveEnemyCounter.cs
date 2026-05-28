using System;
using System.Collections;
using UnityEngine;

public class AliveEnemyCounter : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;

    private int _count;

    public event Action AllEnemiesKilled;

    private void OnEnable()
    {
        foreach(EnemySpawner spawner in _spawners)
        {
            spawner.Getted += IncreaseCount;
        }

        foreach(EnemySpawner spawner in _spawners)
        {
            spawner.Released += DecreaseCount;
        }
    }

    private void OnDisable()
    {
        foreach (EnemySpawner spawner in _spawners)
        {
            spawner.Getted -= IncreaseCount;
        }

        foreach (EnemySpawner spawner in _spawners)
        {
            spawner.Released -= DecreaseCount;
        }
    }

    private void Awake()
    {
        _count = 0;
    }

    private void DecreaseCount()
    {
        _count--;

        if (_count == 0)
            AllEnemiesKilled?.Invoke();
    }

    private void IncreaseCount()
    {
        _count++;
    }
}