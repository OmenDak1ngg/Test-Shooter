using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private readonly string SceneName = "BaseScene";

    private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnClick()
    {
        SceneManager.LoadScene(SceneName);
    }
}