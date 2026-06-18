using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Aetherbound/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    [TextArea(3, 10)]
    public string skillDescription;
    public Sprite skillIcon;
    public int skillLevelRequirement;
    public SkillData[] prerequisiteSkills;

    // Add other skill-related properties here, such as:
    // public float manaCost;
    // public float damageAmount;
    // public GameObject skillEffectPrefab;
}
