using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A simple struct to hold a stat type and its modification value.
/// Used by races, classes, and equipment.
/// We are keeping it in this file since it is not a public class and can be used by other files as well
/// </summary>
[System.Serializable]
public struct StatModifier
{
    public StatType stat;
    public float value;
}

[CreateAssetMenu(fileName = "New Race", menuName = "Aetherbound/Character/Race")]
public class RaceData : ScriptableObject
{
    [Header("Race Info")]
    public string raceName;
    public Race raceType;
    [TextArea(3, 5)]
    public string description;

    [Header("Stat Bonuses")]
    [Tooltip("The base stat modifiers this race provides.")]
    public List<StatModifier> baseStatModifiers;

    [Header("Evolution")]
    [Tooltip("The total Race XP required to evolve to the next stage.")]
    public long xpToEvolve = -1; // -1 indicates this race stage cannot evolve further
    [Tooltip("The next racial form this race evolves into.")]
    public RaceData evolutionTarget;

}
