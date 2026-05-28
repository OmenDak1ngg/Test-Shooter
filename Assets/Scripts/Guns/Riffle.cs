using System;
using System.Collections;
using UnityEngine;

public class Riffle : Gun
{
    [SerializeField] private float _delayBetweenShots = 0.5f;

    private WaitForSeconds _waitBetweenShots;


    protected override void Awake()
    {
        base.Awake();

        _waitBetweenShots = new WaitForSeconds(_delayBetweenShots);
    }

    protected override void Shoot()
    {
        if (CanShoot == false)
            return;

        base.Shoot();
        CanShoot = false;

        StartCoroutine(WaitBetweenShots());
    }

    private IEnumerator WaitBetweenShots()
    {
        yield return _waitBetweenShots;

        CanShoot = true;
    }
}