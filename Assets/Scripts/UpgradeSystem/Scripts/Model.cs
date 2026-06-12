using UnityEngine;

public class Model : MonoBehaviour
{
    [SerializeField] private int _level;

    public int Level => _level;
}