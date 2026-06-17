using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Defines a requirement for unlocking a class.
/// </summary>
[System.Serializable]
public struct ClassUnlockRequirement
{
    public ClassData requiredClass;
    public int requiredLevel;
}

[CreateAssetMenu(fileName = "New Class", menuName = "Aetherbound/Character/Class")]
public class ClassData : ScriptableObject
{
    [Header("Class Info")]
    public string className;
    [TextArea(3, 5)]
    public string description;
    public Sprite icon;

    [Header("Progression")]
    [Tooltip("The maximum level this class can reach.")]
    public int maxLevel = 20;
    [Tooltip("The stat bonuses granted for each level in this class.")]
    public List<StatModifier> statBonusPerLevel;

    [Header("Mastery")]
    [Tooltip("The permanent, passive stat bonuses granted when this class is mastered (reaches max level).")]
    public List<StatModifier> masteryBonus;

    [Header("Unlocking")]
    [Tooltip("The list of other classes and their required levels to unlock this one. Leave empty for starter classes.")]
    public List<ClassUnlockRequirement> unlockRequirements;

    // We will add a list for unlocked abilities here later
    // public List<AbilityUnlock> unlockedAbilities;
}
