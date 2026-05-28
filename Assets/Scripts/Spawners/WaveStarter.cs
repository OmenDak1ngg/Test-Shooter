using System.Collections;
using UnityEngine;


public class WaveStarter : MonoBehaviour
{
    [SerializeField] private EnemySpawner[] _spawners;
    [SerializeField] private int _baseEnemyCount = 5;
    [SerializeField] private int _bossEnemyCount = 1;

    [SerializeField] private float _delayBetweenWaves = 3f;
    [SerializeField] private AliveEnemyCounter _aliveEnemyCounter; 

    private WaitForSeconds _waitBetweenWaves;

    private void OnEnable()
    {
        _aliveEnemyCounter.AllEnemiesKilled += StartSpawnEnemies;
    }

    private void OnDisable()
    {
        _aliveEnemyCounter.AllEnemiesKilled -= StartSpawnEnemies;
    }

    private void Awake()
    {
        _waitBetweenWaves = new WaitForSeconds(_delayBetweenWaves);
    }

    private void Start()
    {
        StartSpawnEnemies();
    }

    private void StartSpawnEnemies()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return _waitBetweenWaves;

        foreach (EnemySpawner spawner in _spawners)
        {
            if (spawner is BossSpawner)
                spawner.SpawnEnemies(_bossEnemyCount);
            else
                spawner.SpawnEnemies(_baseEnemyCount);
        }
    }
}