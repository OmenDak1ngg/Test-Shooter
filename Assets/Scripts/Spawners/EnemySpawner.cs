using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private Player _player;

    private Transform _playerTransform;

    private List<Enemy> _enemies = new List<Enemy>();

    public event Action Getted;
    public event Action Released;

    private void OnEnable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Health.Killed += () => Release(enemy);
        }
    }

    private void OnDisable()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Health.Killed -= () => Release(enemy);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _playerTransform = _player.transform;
    }

    protected override Enemy OnInstantiate()
    {
        Enemy newEnemy = base.OnInstantiate();
    
        _enemies.Add(newEnemy);
        newEnemy.Health.Killed += () => Release(newEnemy);
        newEnemy.SetupOnCreate(_playerTransform);

        return newEnemy;
    }

    protected override void OnGet(Enemy pooledObject)
    {
        base.OnGet(pooledObject);

        pooledObject.transform.position = _spawnZone.GetRandomPointAtBound();
        pooledObject.Health.ResetHealth();

        pooledObject.Mover.StartMoveToTarget();

        Getted?.Invoke();
    }

    protected override void OnRelease(Enemy pooledObject)
    {
        base.OnRelease(pooledObject);

        Released?.Invoke();
    }

    public void SpawnEnemies(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Get();
        }
    }
}