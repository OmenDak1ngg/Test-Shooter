using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class PatriclesSpawner : Spawner<ParticleSystem>
{
    [SerializeField] private GunSwitcher _gunSwitcher;

    [SerializeField] private Gun[] _guns;

    private float _duration;

    private WaitForSeconds _waitDuration;
    private Transform _spawnTransform;

    private void OnEnable()
    {
        _gunSwitcher.GunSwitched += OnGunSwitched;

        foreach (Gun gun in _guns)
        {
            gun.Shooted += Get;
        }
    }

    private void OnDisable()
    {
        _gunSwitcher.GunSwitched -= OnGunSwitched;

        foreach (Gun gun in _guns)
        {
            gun.Shooted -= Get;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _duration = Prefab.main.duration;
        _waitDuration = new WaitForSeconds(_duration);
    }

    private void Start()
    {

        _spawnTransform = _gunSwitcher.CurrentTransformShootPoint;
    }

    private void OnGunSwitched(Transform shootPointTransform)
    {
        _spawnTransform = shootPointTransform;
    }

    private IEnumerator ReleaseAfterPlay(ParticleSystem particleSystem)
    {
        yield return _waitDuration;

        Release(particleSystem);
    }

    protected override void OnGet(ParticleSystem pooledObject)
    {
        base.OnGet(pooledObject);
        pooledObject.Play();
        pooledObject.transform.position = _spawnTransform.position;
        pooledObject.transform.rotation = _spawnTransform.rotation * Quaternion.Euler(0, 180, 0); ;

        StartCoroutine(ReleaseAfterPlay(pooledObject));
    }

    public void SetSpawnPosition(Transform spawnPosition)
    {
        _spawnTransform = spawnPosition;
    }
}