using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SpawnZone : MonoBehaviour
{
    private readonly int CountOfColliderSides = 4;
    private readonly int HalfDivider = 2;
    
    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    public Vector3 GetRandomPointAtBound()
    {
        Vector3 center = _collider.center;
        Vector3 halfSize = _collider.size / HalfDivider;

        int side = Random.Range(0, CountOfColliderSides);
        Vector3 localPoint = center;

        switch (side)
        {
            case 0:
                localPoint.x += halfSize.x;
                localPoint.z += Random.Range(-halfSize.z, halfSize.z);
                break;

            case 1: 
                localPoint.x -= halfSize.x;
                localPoint.z += Random.Range(-halfSize.z, halfSize.z);
                break;

            case 2: 
                localPoint.x += Random.Range(-halfSize.x, halfSize.x);
                localPoint.z += halfSize.z;
                break;

            case 3: 
                localPoint.x += Random.Range(-halfSize.x, halfSize.x);
                localPoint.z -= halfSize.z;
                break;
        }

        Vector3 worldPoint = transform.TransformPoint(localPoint);
        return worldPoint;
    }
}