using System.Collections.Generic;
using UnityEngine;
using System;

public class ProfessionManager : MonoBehaviour
{
    public static ProfessionManager Instance { get; private set; }

    // Holds the experience for each profession
    private Dictionary<ProfessionData, int> professionExperience = new Dictionary<ProfessionData, int>();
    // Holds the level for each profession
    private Dictionary<ProfessionData, int> professionLevels = new Dictionary<ProfessionData, int>();

    // Event to notify UI of XP changes
    public event Action<ProfessionData, float, float> onExperienceChanged;
    // Event to notify UI of level ups
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
            InitializeProfessions();
        }
    }

    private void InitializeProfessions()
    {
        // Load all ProfessionData objects from the Resources folder (or another location)
        ProfessionData[] allProfessions = Resources.LoadAll<ProfessionData>("Professions");

        foreach (var prof in allProfessions)
        {
            if (!professionExperience.ContainsKey(prof))
            {
                professionExperience[prof] = 0;
                professionLevels[prof] = 1;
            }
        }
    }

    public void AddExperience(ProfessionData profession, int amount)
    {
        if (!professionExperience.ContainsKey(profession))
        {
            Debug.LogWarning($"Profession {profession.professionName} not initialized.");
            return;
        }

        professionExperience[profession] += amount;
        CheckForLevelUp(profession);

        onExperienceChanged?.Invoke(profession, GetExperience(profession), GetXPForNextLevel(profession));
    }

    private void CheckForLevelUp(ProfessionData profession)
    {
        int currentLevel = GetLevel(profession);
        int xpForNextLevel = GetXPForNextLevel(profession, currentLevel);

        if (professionExperience[profession] >= xpForNextLevel)
        {
            professionLevels[profession]++;
            // Optionally, you can carry over excess XP
            // professionExperience[profession] -= xpForNextLevel;
            onLevelUp?.Invoke(profession, GetLevel(profession));
            Debug.Log($"{profession.professionName} leveled up to {GetLevel(profession)}!");
            CheckForLevelUp(profession); // In case of multiple level ups
        }
    }

    public int GetLevel(ProfessionData profession)
    {
        return professionLevels.ContainsKey(profession) ? professionLevels[profession] : 1;
    }

    public int GetExperience(ProfessionData profession)
    {
        return professionExperience.ContainsKey(profession) ? professionExperience[profession] : 0;
    }

    public int GetXPForNextLevel(ProfessionData profession, int? level = null)
    {
        int currentLevel = level ?? GetLevel(profession);
        // A simple exponential formula, but you can customize this
        return Mathf.FloorToInt(100 * Mathf.Pow(1.5f, currentLevel - 1));
    }
}
