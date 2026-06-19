using UnityEngine;
using System.Collections.Generic;

public class ClassManager : MonoBehaviour
{
    public static ClassManager Instance { get; private set; }

    [Header("Player Classes")]
    public ClassData currentClass;
    public Dictionary<ClassData, int> classLevels = new Dictionary<ClassData, int>();
    public Dictionary<ClassData, int> classExperience = new Dictionary<ClassData, int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if (currentClass != null && !classLevels.ContainsKey(currentClass))
        {
            classLevels[currentClass] = 1;
        }
    }

    public void SwitchClass(ClassData newClass)
    {
        if (IsClassUnlocked(newClass))
        {
            currentClass = newClass;
            if (!classLevels.ContainsKey(newClass))
            {
                classLevels[newClass] = 1;
            }

            var playerStats = GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.RecalculateAllStats();
            }

            Debug.Log("Switched to class: " + newClass.className);
        }
        else
        {
            Debug.LogWarning("Attempted to switch to a locked class: " + newClass.className);
        }
    }

    public bool IsClassUnlocked(ClassData classData)
    {
        foreach (var requirement in classData.unlockRequirements)
        {
            if (!classLevels.ContainsKey(requirement.requiredClass) || classLevels[requirement.requiredClass] < requirement.requiredLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void AddExperience(int amount)
    {
        if (currentClass == null) return;

        if (!classExperience.ContainsKey(currentClass))
        {
            classExperience[currentClass] = 0;
        }
        classExperience[currentClass] += amount;

        // TODO: Add logic for leveling up
    }

    public int GetClassLevel(ClassData classData)
    {
        if (classData == null) return 0;
        return classLevels.TryGetValue(classData, out int level) ? level : 1;
    }

    public List<StatModifier> GetActiveClassStatBonus()
    {
        if (currentClass == null) return new List<StatModifier>();
        return ConvertStatBonuses(currentClass.statBonuses, GetClassLevel(currentClass));
    }

    public List<StatModifier> GetTotalMasteryBonuses()
    {
        var bonuses = new List<StatModifier>();
        foreach (var entry in classLevels)
        {
            if (entry.Key == currentClass || entry.Value <= 1) continue;

            foreach (var modifier in ConvertStatBonuses(entry.Key.statBonuses, entry.Value - 1))
            {
                bonuses.Add(new StatModifier
                {
                    stat = modifier.stat,
                    value = modifier.value * 0.25f
                });
            }
        }
        return bonuses;
    }

    private static List<StatModifier> ConvertStatBonuses(List<StatBonus> statBonuses, int level)
    {
        var modifiers = new List<StatModifier>();
        if (statBonuses == null || level <= 0) return modifiers;

        foreach (var bonus in statBonuses)
        {
            modifiers.Add(new StatModifier
            {
                stat = bonus.stat,
                value = bonus.value * level
            });
        }
        return modifiers;
    }
}
