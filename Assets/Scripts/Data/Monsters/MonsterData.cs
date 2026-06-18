using UnityEngine;
using System.Collections.Generic;

// A struct used by MonsterData, MonsterStats, and other systems.
// It's defined here for clarity, but could be in a shared file like Enums.cs
[System.Serializable]
public struct StatModifier
{
    public Stat stat;
    public float value;
}

[System.Serializable]
public struct LootDrop
{
    public ItemData item;
    [Range(0f, 1f)] public float dropChance;
}

[CreateAssetMenu(fileName = "New MonsterData", menuName = "AetherBound/Data/Monster")]
public class MonsterData : ScriptableObject
{
    [Header("Info")]
    public string monsterName;
    public GameObject monsterPrefab;

    [Header("Base Stats")]
    public List<StatModifier> baseStats = new List<StatModifier>();

    [Header("Leveling")]
    public int maxLevel = 50;
    public AnimationCurve xpCurve;
    public List<StatModifier> statsPerLevel = new List<StatModifier>();

    [Header("Night Effects")]
    [Tooltip("Stat multipliers applied at night. E.g., a value of 1.2 is a 20% boost.")]
    public List<StatModifier> nightStatBoosts = new List<StatModifier>();
    public List<LootDrop> rareLootTable = new List<LootDrop>();

    [Header("Skills & Abilities")]
    public List<SkillData> skills = new List<SkillData>();

    [Header("Standard Loot Table")]
    public List<LootDrop> lootTable = new List<LootDrop>();
    public int minGoldDrop = 0;
    public int maxGoldDrop = 10;
}
