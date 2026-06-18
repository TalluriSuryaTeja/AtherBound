using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public List<SkillData> learnedSkills = new List<SkillData>();

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

    public bool IsSkillLearned(SkillData skill)
    {
        return learnedSkills.Contains(skill);
    }

    public bool CanLearnSkill(SkillData skill)
    {
        if (IsSkillLearned(skill)) return false; // Already learned

        // Check level requirement (assuming a PlayerStats manager exists)
        if (PlayerStats.Instance.level < skill.skillLevelRequirement) return false;

        // Check for prerequisite skills
        foreach (var prerequisite in skill.prerequisiteSkills)
        {
            if (!IsSkillLearned(prerequisite))
            {
                return false; // Missing a prerequisite
            }
        }

        return true;
    }

    public void LearnSkill(SkillData skill)
    {
        if (CanLearnSkill(skill))
        {
            learnedSkills.Add(skill);
            // You might want to trigger an event here to update the UI or other systems
            Debug.Log($"Learned skill: {skill.skillName}");
        }
        else
        {
            Debug.LogWarning($"Cannot learn skill: {skill.skillName}. Requirements not met.");
        }
    }
}
