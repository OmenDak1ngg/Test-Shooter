using System;
using UnityEngine;

[Serializable]
public class AttributeData
{
    public AttributeType type;

    public int[] experiencePerLevel;
    [Min(0)] public int level = 0;
    [Min(0)] public int currentExperience = 0;
    [Min(1)] public int experienceToUpgrade = 100;
    [Min(1)] public int addedExperience = 10;
}