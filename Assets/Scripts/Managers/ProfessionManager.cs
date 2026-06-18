using UnityEngine;
using System.Collections.Generic;

public class ProfessionManager : MonoBehaviour
{
    public static ProfessionManager Instance { get; private set; }

    public List<ProfessionData> professions = new List<ProfessionData>();

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
        // TODO: Add logic for leveling up
    }
}
