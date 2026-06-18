using UnityEngine;
using System.Collections.Generic;

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
