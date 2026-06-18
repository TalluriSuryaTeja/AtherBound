/// <summary>
/// A simple struct to hold a stat type and its modification value.
/// Used by races, classes, equipment, and monsters.
/// </summary>
[System.Serializable]
public struct StatModifier
{
    public StatType stat;
    public float value;
}
