using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MonsterStats : MonoBehaviour
{
    public MonsterData monsterData;

    [Header("Current Stats")]
    public int currentLevel = 1;
    public float currentXP = 0;
    public float currentHealth;
    public float currentMana;

    private Dictionary<Stat, float> finalStats = new Dictionary<Stat, float>();
    private bool isNight = false;

    void OnEnable()
    {
        GameManager.OnTimeOfDayChanged += HandleTimeOfDayChange;
    }

    void OnDisable()
    {
        GameManager.OnTimeOfDayChanged -= HandleTimeOfDayChange;
    }

    void Awake()
    {
        // Initial stat calculation
        CalculateStats();
        currentHealth = GetStat(StatType.Health);
        currentMana = GetStat(StatType.Mana);
    }

    private void HandleTimeOfDayChange(GameManager.TimeOfDay newTimeOfDay)
    {
        isNight = newTimeOfDay == GameManager.TimeOfDay.Night;
        // Recalculate stats with night modifiers
        CalculateStats();
        Debug.Log($"{monsterData.monsterName} has been {(isNight ? "empowered" : "weakened")} by the {(isNight ? "night" : "day")}.");
    }

    public void CalculateStats()
    {
        finalStats.Clear();

        // 1. Base Stats
        foreach (var statMod in monsterData.baseStats)
        {
            finalStats[statMod.stat] = statMod.value;
        }

        // 2. Stats from Leveling
        for (int i = 1; i < currentLevel; i++)
        {
            foreach (var statMod in monsterData.statsPerLevel)
            {
                if (finalStats.ContainsKey(statMod.stat))
                {
                    finalStats[statMod.stat] += statMod.value;
                }
                else
                {
                    finalStats[statMod.stat] = statMod.value;
                }
            }
        }

        // 3. Night-time Modifiers
        if (isNight)
        {
            foreach (var nightMod in monsterData.nightStatBoosts)
            {
                if (finalStats.ContainsKey(nightMod.stat))
                {
                    // Assuming the modifier is a multiplier, e.g., 1.2 for a 20% boost
                    finalStats[nightMod.stat] *= nightMod.value;
                }
            }
        }
    }

    public float GetStat(StatType statType)
    {
        Stat statObject = finalStats.Keys.FirstOrDefault(s => s.statType == statType);
        if (statObject != null)
        {
            return finalStats[statObject];
        }
        return 0f;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{monsterData.monsterName} has been defeated!");

        // Loot Drop Logic
        List<LootDrop> lootTable = isNight ? monsterData.rareLootTable : monsterData.lootTable;
        if (lootTable != null && lootTable.Count > 0)
        {
            foreach (var drop in lootTable)
            {
                if (Random.value <= drop.dropChance) // Random.value is between 0.0 and 1.0
                {
                    // TODO: Implement inventory logic to add item to player
                    Debug.Log($"Dropped {drop.item.name}!");
                }
            }
        }

        Destroy(gameObject);
    }
}
