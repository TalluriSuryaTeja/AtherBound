// This file contains all project-wide enums for easy access and management.

// Defines the type of damage, used for calculating resistances.
public enum DamageType
{
    Physical,
    Fire,
    Water,
    Earth,
    Air,
    Arcane
}

// The list of all stats that can be modified by races, classes, gear, and buffs.
public enum StatType
{
    // --- Primary Core Stats ---
    Strength,       // Affects Melee Damage, Armor
    Dexterity,      // Affects Crit Chance, Attack Speed, Dodge
    Intelligence,   // Affects Magic Damage, Max Mana, Mana Regen
    Vitality,       // Affects Max Health, Health Regen

    // --- Secondary Derived Stats ---
    // (These can also be directly modified by items and buffs)

    // Resources
    MaxHealth,
    MaxMana,
    HealthRegen,
    ManaRegen,
    
    // Offensive
    MeleeDamage,
    MagicDamage,
    AttackSpeed,
    CritChance,
    CritDamage,

    // Defensive
    Armor,          // Reduces incoming Physical damage
    MagicDefense,   // Reduces incoming magical damage (general)
    DodgeChance,
    
    // Elemental Resistances
    FireResistance,
    WaterResistance,
    EarthResistance,
    AirResistance,
    ArcaneResistance
}

// Defines the base race of a character. Used for identifying racial trees.
public enum Race
{
    Human,
    Dwarf,
    Elf
}

// Categories for filtering items in the inventory UI.
public enum ItemCategory
{
    All,
    Weapon,
    Armor,
    Accessory,
    Consumable,
    Material,
    QuestItem,
    Tool
}

// Specific slots where equipment can be placed.
public enum EquipmentSlot
{
    // Armor
    Head,
    Body,
    Hands,
    Legs,
    Feet,
    
    // Accessories
    Ring1,
    Ring2,
    Necklace,

    // Tools & Weapons
    MainHand, // For weapons or primary tools
    OffHand,  // For shields or secondary tools
}

// Defines the rarity of an item, used for color-coding and sorting.
public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
