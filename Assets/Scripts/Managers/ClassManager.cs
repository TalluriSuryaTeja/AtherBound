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
    }

    public void SwitchClass(ClassData newClass)
    {
        if (IsClassUnlocked(newClass))
        {
            currentClass = newClass;
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
}
