using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [SerializeField] private List<AttributeData> _attributes = new();

    public event Action<AttributeType> AttributeChanged;
    public event Action<AttributeType, int> AttributeUpgraded;

    private void Awake()
    {
        //Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public int GetLevel(AttributeType type)
    {
        AttributeData data = GetData(type);
        return data != null ? data.level : 0;
    }

    public void AddExperience(AttributeType type)
    {
        AttributeData data = GetData(type);
        if (data == null) return;

        data.currentExperience += data.addedExperience;
        AttributeChanged?.Invoke(type);

        Save();
    }

    public int GetCurrentExperience(AttributeType type)
    {
        AttributeData data = GetData(type);
        return data != null ? data.currentExperience : 0;
    }

    public void TryUpgrade(AttributeType type)
    {
        AttributeData data = GetData(type);

        if (data == null)
            return;

        int maxLevel = data.experiencePerLevel.Length;

        bool upgraded = false;

        while (data.currentExperience >= data.experienceToUpgrade &&
               data.level < maxLevel)
        {
            data.currentExperience -= data.experienceToUpgrade;
            data.level++;

            if (data.level < maxLevel)
                data.experienceToUpgrade = GetExpirienceFoLevel(data);

            upgraded = true;
        }

        if (upgraded)
        {
            AttributeChanged?.Invoke(type);
            AttributeUpgraded?.Invoke(type, data.level);

            Save();
        }
    }

    private int GetExpirienceFoLevel(AttributeData data)
    {
        if (data.level <= 0)
            return data.experiencePerLevel[0];

        if (data.level - 1 >= data.experiencePerLevel.Length)
            return data.experiencePerLevel[^1];

        return data.experiencePerLevel[data.level - 1];
    }

    private void Save()
    {
        UpgradeSaveData saveData = new UpgradeSaveData();

        foreach (var attr in _attributes)
        {
            saveData.attributes.Add(new AttributeSaveEntry
            {
                type = attr.type,
                level = attr.level,
                currentExperience = attr.currentExperience,
                experienceToUpgrade = attr.experienceToUpgrade
            });
        }

        SaveSystem.Save(saveData);
    }

    private void Load()
    {
        UpgradeSaveData saveData = SaveSystem.Load();

        if (saveData == null || saveData.attributes == null)
            return;

        foreach (var saved in saveData.attributes)
        {
            AttributeData data = GetData(saved.type);
            if (data == null) continue;

            data.level = saved.level;
            data.currentExperience = saved.currentExperience;
            data.experienceToUpgrade = saved.experienceToUpgrade;
        }
    }

    private AttributeData GetData(AttributeType type)
    {
        return _attributes.Find(a => a.type == type);
    }
}