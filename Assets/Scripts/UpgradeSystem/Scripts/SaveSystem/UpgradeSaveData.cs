using System;
using System.Collections.Generic;

[Serializable]
public class AttributeSaveEntry
{
    public AttributeType type;
    public int level;
    public int currentExperience;
    public int experienceToUpgrade;
}

[Serializable]
public class UpgradeSaveData
{
    public List<AttributeSaveEntry> attributes = new();

    public string hash;
}