using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents an item that can be equipped to a character.
/// </summary>
[CreateAssetMenu(fileName = "New Equipment", menuName = "Aetherbound/Items/Equipment")]
public class EquipmentData : ItemData
{
    [Header("Equipment Info")]
    public EquipmentSlot equipmentSlot;

    [Header("Stat Modifiers")]
    [Tooltip("The stat bonuses this item provides when equipped.")]
    public List<StatModifier> statModifiers;

    // We can add weapon-specific stats here later if needed
    // public float baseDamage;
    // public float attackSpeed;
}
