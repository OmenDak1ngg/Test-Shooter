using UnityEngine;

public class ModelChanger : MonoBehaviour, IUpgradable
{
    [SerializeField] private Model[] _models;

    private Model _currentModel;

    private void Awake()
    {
        if (_models.Length == 0)
            return;

        foreach (var model in _models)
            model.gameObject.SetActive(false);

        _currentModel = _models[0];
        _currentModel.gameObject.SetActive(true);
    }

    public void UpgradeByLevel(int level)
    {
        Debug.Log($"UpgradeByLevel вызван. Уровень = {level}");
        Model matchingModel = null;
        int maxModelLevel = -1;

        foreach (var model in _models)
        {
            if (model.Level <= level && model.Level > maxModelLevel)
            {
                maxModelLevel = model.Level;
                matchingModel = model;
            }
        }

        if (matchingModel == null || matchingModel == _currentModel)
            return;

        Vector3 position = _currentModel.transform.position;
        Quaternion rotation = _currentModel.transform.rotation;
        Vector3 scale = _currentModel.transform.localScale;

        _currentModel.gameObject.SetActive(false);

        matchingModel.transform.position = position;
        matchingModel.transform.rotation = rotation;
        matchingModel.transform.localScale = scale;

        matchingModel.gameObject.SetActive(true);

        _currentModel = matchingModel;
    }
}