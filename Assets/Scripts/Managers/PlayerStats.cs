using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public event Action onStatsChanged;

    [Header("Core Identity")]
    public RaceData currentRaceData;
    public int playerLevel = 1;
    public long raceXP = 0;

    [Header("Core Stats (Player-Assigned)")]
    public int assignedStrength = 10;
    public int assignedDexterity = 10;
    public int assignedIntelligence = 10;
    public int assignedVitality = 10;

    [Header("Final Calculated Stats")]
    // Core Stats
    public float finalStrength;
    public float finalDexterity;
    public float finalIntelligence;
    public float finalVitality;
    // Resources
    public float finalMaxHealth;
    public float finalMaxMana;
    public float finalHealthRegen;
    public float finalManaRegen;
    // Offensive
    public float finalMeleeDamage;
    public float finalMagicDamage;
    public float finalAttackSpeed;
    public float finalCritChance;
    public float finalCritDamage;
    // Defensive
    public float finalArmor;
    public float finalMagicDefense;
    public float finalDodgeChance;
    // Resistances
    public float finalFireResistance;
    public float finalWaterResistance;
    public float finalEarthResistance;
    public float finalAirResistance;
    public float finalArcaneResistance;
    
    // --- Private fields for managers ---
    private ClassManager classManager;
    private EquipmentManager equipmentManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        classManager = GetComponent<ClassManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        
        RecalculateAllStats();
    }

    private Dictionary<StatType, float> GetBaseStats()
    {
        return new Dictionary<StatType, float>
        {
            { StatType.Strength, assignedStrength },
            { StatType.Dexterity, assignedDexterity },
            { StatType.Intelligence, assignedIntelligence },
            { StatType.Vitality, assignedVitality }
        };
    }

    private void AddModifiersToDict(Dictionary<StatType, float> dict, List<StatModifier> modifiers)
    {
        if (modifiers == null) return;
        foreach (var modifier in modifiers)
        {
            if (!dict.ContainsKey(modifier.stat))
            {
                dict[modifier.stat] = 0;
            }
            dict[modifier.stat] += modifier.value;
        }
    }

    public void RecalculateAllStats()
    {
        var stats = GetBaseStats();

        if (currentRaceData != null) AddModifiersToDict(stats, currentRaceData.baseStatModifiers);
        if (classManager != null)
        {
            AddModifiersToDict(stats, classManager.GetActiveClassStatBonus());
            AddModifiersToDict(stats, classManager.GetTotalMasteryBonuses());
        }
        if (equipmentManager != null)
        {
            AddModifiersToDict(stats, equipmentManager.GetAllEquippedStatBonuses());
        }

        finalStrength = stats.GetValueOrDefault(StatType.Strength);
        finalDexterity = stats.GetValueOrDefault(StatType.Dexterity);
        finalIntelligence = stats.GetValueOrDefault(StatType.Intelligence);
        finalVitality = stats.GetValueOrDefault(StatType.Vitality);

        finalMaxHealth = stats.GetValueOrDefault(StatType.MaxHealth) + (finalVitality * 10);
        finalMaxMana = stats.GetValueOrDefault(StatType.MaxMana) + (finalIntelligence * 10);
        finalHealthRegen = stats.GetValueOrDefault(StatType.HealthRegen) + (finalVitality * 0.1f);
        finalManaRegen = stats.GetValueOrDefault(StatType.ManaRegen) + (finalIntelligence * 0.1f);

        finalMeleeDamage = stats.GetValueOrDefault(StatType.MeleeDamage) + (finalStrength * 2);
        finalMagicDamage = stats.GetValueOrDefault(StatType.MagicDamage) + (finalIntelligence * 2);
        finalAttackSpeed = stats.GetValueOrDefault(StatType.AttackSpeed) + (finalDexterity * 0.01f);
        finalCritChance = stats.GetValueOrDefault(StatType.CritChance) + (finalDexterity * 0.05f);
        finalCritDamage = stats.GetValueOrDefault(StatType.CritDamage) + 1.5f; 

        finalArmor = stats.GetValueOrDefault(StatType.Armor) + (finalStrength * 0.5f);
        finalMagicDefense = stats.GetValueOrDefault(StatType.MagicDefense) + (finalIntelligence * 0.5f);
        finalDodgeChance = stats.GetValueOrDefault(StatType.DodgeChance) + (finalDexterity * 0.02f);
        
        finalFireResistance = stats.GetValueOrDefault(StatType.FireResistance);
        finalWaterResistance = stats.GetValueOrDefault(StatType.WaterResistance);
        finalEarthResistance = stats.GetValueOrDefault(StatType.EarthResistance);
        finalAirResistance = stats.GetValueOrDefault(StatType.AirResistance);
        finalArcaneResistance = stats.GetValueOrDefault(StatType.ArcaneResistance);

        Debug.Log("Player stats have been recalculated with full logic.");
        onStatsChanged?.Invoke();
    }

    public void AddRaceXP(long amount)
    {
        raceXP += amount;
        if (currentRaceData != null && currentRaceData.xpToEvolve != -1 && raceXP >= currentRaceData.xpToEvolve)
        {
            EvolveRace();
        }
    }

    private void EvolveRace()
    {
        if (currentRaceData.evolutionTarget != null)
        {
            Debug.Log($"RACE EVOLVED! From {currentRaceData.raceName} to {currentRaceData.evolutionTarget.raceName}");
            currentRaceData = currentRaceData.evolutionTarget;
            RecalculateAllStats(); 
        }
    }

    public void TakeDamage(float amount, DamageType type)
    {
    }
}

public static class DictionaryExtensions
{
    public static float GetValueOrDefault(this Dictionary<StatType, float> dict, StatType key)
    {
        return dict.TryGetValue(key, out float value) ? value : 0;
    }
}