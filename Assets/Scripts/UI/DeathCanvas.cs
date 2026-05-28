using UnityEngine;
using UnityEngine.UI;


public class DeathCanvas : MonoBehaviour
{
    [SerializeField] private Image _deathWindow;

    private void Awake()
    {
        _deathWindow.gameObject.SetActive(false);
    }

    public void ShowDeathWindow()
    {
        _deathWindow.gameObject.SetActive(true);
    }
}