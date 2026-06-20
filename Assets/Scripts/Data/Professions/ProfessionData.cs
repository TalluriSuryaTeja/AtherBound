using UnityEngine;

[CreateAssetMenu(fileName = "New Profession", menuName = "Aetherbound/Data/Profession")]
public class ProfessionData : ScriptableObject
{
    [Header("Profession Info")]
    public string professionName;
    public string description;
    public Sprite icon;

    [Header("Progression")]
    public int level;
    public int experience;
    public AnimationCurve xpCurve;
}
