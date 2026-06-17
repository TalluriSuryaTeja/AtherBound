using UnityEngine;
using System.Collections.Generic;

public class ClassManager : MonoBehaviour
{
    [Header("Active Class")]
    public ClassData activeClass;

    [Header("Class Progress")]
    public Dictionary<ClassData, int> classLevels = new Dictionary<ClassData, int>();
    public Dictionary<ClassData, float> classXP = new Dictionary<ClassData, float>();

    [Header("Mastery")]
    public HashSet<ClassData> masteredClasses = new HashSet<ClassData>();

    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    /// <summary>
    /// Sets a new class as the active one and recalculates stats.
    /// </summary>
    public void SwitchClass(ClassData newClass)
    {
        if (newClass == null || !IsClassUnlocked(newClass)) 
        {
            Debug.LogWarning($"Cannot switch to {newClass?.className ?? "null"}. Class is not unlocked.");
            return;
        }

        if (activeClass != newClass)
        {
            activeClass = newClass;
            playerStats.RecalculateAllStats(); 
            Debug.Log($"Switched to {newClass.className} class.");
        }
    }

    /// <summary>
    /// Adds XP to the currently active class and handles leveling up.
    /// </summary>
    public void AddClassXP(float amount)
    {
        if (activeClass == null) return;

        int currentLevel = GetClassLevel(activeClass);
        if (currentLevel >= activeClass.maxLevel) return; // Cannot gain XP if max level

        if (!classXP.ContainsKey(activeClass))
        {
            classXP[activeClass] = 0;
        }
        classXP[activeClass] += amount;

        float xpForNextLevel = 100 * Mathf.Pow(1.1f, currentLevel); // Example formula: 100, 110, 121, ...

        while (classXP[activeClass] >= xpForNextLevel)
        {
            classXP[activeClass] -= xpForNextLevel;
            LevelUp(activeClass);
            currentLevel = GetClassLevel(activeClass);
            xpForNextLevel = 100 * Mathf.Pow(1.1f, currentLevel);
        }
    }

    private void LevelUp(ClassData classToLevel)
    {
        if (!classLevels.ContainsKey(classToLevel))
        {
            classLevels[classToLevel] = 0;
        }

        classLevels[classToLevel]++;
        int newLevel = classLevels[classToLevel];
        Debug.Log($"{classToLevel.className} leveled up to {newLevel}!");

        // Check for mastery
        if (newLevel >= classToLevel.maxLevel)
        {
            if (masteredClasses.Add(classToLevel))
            {
                Debug.LogWarning($"CLASS MASTERED: {classToLevel.className}! Permanent mastery bonus unlocked.");
            }
        }

        // After any level up, recalculate stats
        playerStats.RecalculateAllStats();
    }
    
    /// <summary>
    /// Gets the current level for a given class.
    /// </summary>
    public int GetClassLevel(ClassData classData)
    {
        return classLevels.TryGetValue(classData, out int level) ? level : 0;
    }

    /// <summary>
    /// Checks if a player meets the requirements to use a class.
    /// </summary>
    public bool IsClassUnlocked(ClassData classData)
    {
        if (classData.unlockRequirements == null || classData.unlockRequirements.Count == 0)
        {
            return true; // Starter class
        }

        foreach (var req in classData.unlockRequirements)
        {
            if (GetClassLevel(req.requiredClass) < req.requiredLevel)
            {
                return false; // Requirement not met
            }
        }

        return true;
    }

    /// <summary>
    /// Calculates the total stat bonus from the currently active class and its level.
    /// </summary>
    public List<StatModifier> GetActiveClassStatBonus()
    {
        List<StatModifier> bonuses = new List<StatModifier>();
        if (activeClass != null)
        {
            int level = GetClassLevel(activeClass);
            if (level > 0)
            {
                foreach (var modifier in activeClass.statBonusPerLevel)
                {
                    bonuses.Add(new StatModifier { stat = modifier.stat, value = modifier.value * level });
                }
            }
        }
        return bonuses;
    }

    /// <summary>"""
    /// Calculates the total permanent stat bonuses from all mastered classes.
    /// </summary>
    public List<StatModifier> GetTotalMasteryBonuses()
    {
        List<StatModifier> totalBonuses = new List<StatModifier>();
        foreach (ClassData masteredClass in masteredClasses)
        {
            if(masteredClass.masteryBonus != null)
            {
                totalBonuses.AddRange(masteredClass.masteryBonus);
            }
        }
        return totalBonuses;
    }
}
