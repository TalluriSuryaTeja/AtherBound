using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New MonsterData", menuName = "Aetherbound/Data/Monster")]
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
