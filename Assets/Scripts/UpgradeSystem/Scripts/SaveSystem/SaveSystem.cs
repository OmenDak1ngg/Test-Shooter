using System.IO;
using System.Text;
using UnityEngine;

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "upgrade_save.json");

    public static void Save(UpgradeSaveData data)
    {
        string saveString = BuildSaveString(data);
        data.hash = HashUtility.GenerateHash(saveString);

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
    }

    public static UpgradeSaveData Load()
    {
        if (!File.Exists(SavePath))
            return null;

        string json = File.ReadAllText(SavePath);

        UpgradeSaveData data = JsonUtility.FromJson<UpgradeSaveData>(json);

        string saveString = BuildSaveString(data);

        string currentHash = HashUtility.GenerateHash(saveString);

        if (currentHash != data.hash)
        {
            throw new InvalidDataException("save file doesn't match");
        }

        return data;
    }

    private static string BuildSaveString(UpgradeSaveData data)
    {
        StringBuilder builder = new StringBuilder();

        foreach (var attribute in data.attributes)
        {
            builder.Append(attribute.type);
            builder.Append(attribute.level);
            builder.Append(attribute.currentExperience);
        }

        return builder.ToString();
    }
    public static void DeleteSave()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
    }

}