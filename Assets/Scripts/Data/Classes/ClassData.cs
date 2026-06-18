using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Class", menuName = "Aetherbound/Data/Class")]
public class ClassData : ScriptableObject
{
    [Header("Class Info")]
    public string className;
    public string description;
    public Sprite icon;

    [Header("Stat Bonuses")]
    public List<StatBonus> statBonuses;

    [Header("Unlock Requirements")]
    public List<ClassRequirement> unlockRequirements;
}

[System.Serializable]
public class StatBonus
{
    public StatType stat;
    public int value;
}

[System.Serializable]
public class ClassRequirement
{
    public ClassData requiredClass;
    public int requiredLevel;
}

public enum StatType
{
    Strength,
    Dexterity,
    Intelligence,
    Vitality
}
