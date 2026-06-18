using UnityEngine;

[CreateAssetMenu(fileName = "New Profession", menuName = "Aetherbound/Profession")]
public class ProfessionData : ScriptableObject
{
    public string professionName;
    [TextArea(3, 10)]
    public string professionDescription;
}
