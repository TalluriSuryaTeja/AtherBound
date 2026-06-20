using UnityEngine;
using System.Collections.Generic;
using System;

public class ProfessionManager : MonoBehaviour
{
    public static ProfessionManager Instance { get; private set; }

    public List<ProfessionData> professions = new List<ProfessionData>();

    public event Action<ProfessionData, int, int> onExperienceChanged;
    public event Action<ProfessionData, int> onLevelUp;

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
    }

    public void AddExperience(ProfessionData profession, int amount)
    {
        profession.experience += amount;
        onExperienceChanged?.Invoke(profession, profession.experience, GetXPForNextLevel(profession));

        while (profession.experience >= GetXPForNextLevel(profession))
        {
            profession.experience -= GetXPForNextLevel(profession);
            profession.level++;
            onLevelUp?.Invoke(profession, profession.level);
            onExperienceChanged?.Invoke(profession, profession.experience, GetXPForNextLevel(profession));
        }
    }

    public int GetExperience(ProfessionData profession)
    {
        return profession.experience;
    }

    public int GetLevel(ProfessionData profession)
    {
        return profession.level;
    }

    public int GetXPForNextLevel(ProfessionData profession)
    {
        if (profession.level == 0) return Mathf.FloorToInt(profession.xpCurve.Evaluate(0));
        return Mathf.FloorToInt(profession.xpCurve.Evaluate(profession.level));
    }
}
