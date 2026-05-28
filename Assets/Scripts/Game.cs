using System.Collections;
using UnityEngine;


public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UserInput _userInput;

    [SerializeField] private DeathCanvas _deathCanvas;

    private void OnEnable()
    {
        if (_player.Health == null)
            return;

        _player.Health.Killed += OnPlayerKilled;
    }

    private void OnDisable()
    {
        _player.Health.Killed -= OnPlayerKilled;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _player.Health.Killed += OnPlayerKilled;
    }

    private void OnPlayerKilled()
    {
        _deathCanvas.ShowDeathWindow();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        _userInput.DeactivateControls();
    }
}