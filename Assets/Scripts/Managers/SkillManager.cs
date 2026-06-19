using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillData> learnedSkills = new List<SkillData>();

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public bool IsSkillLearned(SkillData skill)
    {
        return learnedSkills.Contains(skill);
    }

    public bool CanLearnSkill(SkillData skill)
    {
        if (IsSkillLearned(skill)) return false; // Already learned

        if (playerStats == null || playerStats.playerLevel < skill.skillLevelRequirement) return false;

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
