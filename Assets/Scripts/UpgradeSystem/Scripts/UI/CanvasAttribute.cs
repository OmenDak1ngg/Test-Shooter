using TMPro;
using UnityEngine;

public class CanvasAttribute : MonoBehaviour
{
    [SerializeField] private UserInput _userInput;
    [SerializeField] private CharacterAttributes _attributes;
    [SerializeField] private AttributeType _attributeType;

    [SerializeField] private TextMeshProUGUI _TMPAttributeName;
    [SerializeField] private TextMeshProUGUI _TMPCurrentExpirience;
    [SerializeField] private TextMeshProUGUI _TMPCurrentLevel;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;

        Refresh();
    }

    private void OnEnable()
    {
        _userInput.ClickedShowUpgrades += ChangeState;
        _attributes.AttributeChanged += OnAttributeChanged;
    }

    private void OnDisable()
    {
        _userInput.ClickedShowUpgrades -= ChangeState;
        _attributes.AttributeChanged -= OnAttributeChanged;
    }

    private void OnAttributeChanged(AttributeType type)
    {
        if (type != _attributeType)
            return;

        Refresh();
    }

    private void ChangeState()
    {
        _canvas.enabled = !_canvas.enabled;
    }

    private void Refresh()
    {
        _TMPAttributeName.text = _attributeType.ToString();
        _TMPCurrentExpirience.text = _attributes.GetCurrentExperience(_attributeType).ToString();
        _TMPCurrentLevel.text = _attributes.GetLevel(_attributeType).ToString();
    }
}