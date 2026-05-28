using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T Prefab;

    protected ObjectPool<T> Pool;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => OnInstantiate(),
            actionOnGet: (pooledObject) => OnGet(pooledObject),
            actionOnRelease:(pooledObject) => OnRelease(pooledObject),
            actionOnDestroy: (pooledObejct) => OnDestroyObjcet(pooledObejct)
            );
    }

    protected virtual T OnInstantiate()
    {
        T newObject = Instantiate(Prefab);
        newObject.transform.parent = transform;

        return newObject;
    }

    protected virtual void OnGet(T pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyObjcet(T pooledObejct)
    {
        Destroy(pooledObejct);
    }

    protected virtual void Release(T pooledObject)
    {
        Pool.Release(pooledObject);
    }

    protected virtual void Get()
    {
        Pool.Get();
    }
}
