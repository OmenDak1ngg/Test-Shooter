using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Health))]
public class Character : MonoBehaviour
{
    public Health Health { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
    }
}