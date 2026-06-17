using UnityEngine;

/// <summary>
/// The abstract base class for all items in the game.
/// </summary>
public abstract class ItemData : ScriptableObject
{
    [Header("Item Base Info")]
    public string itemName;
    [TextArea(3, 5)]
    public string description;
    public Sprite itemIcon;
    public ItemRarity rarity;
    public int itemLevel;

    [Header("Stacking")]
    public bool isStackable = true;
    public int maxStackSize = 99;
}
