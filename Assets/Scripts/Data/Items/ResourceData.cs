using UnityEngine;

/// <summary>
/// The abstract base class for all items in the game.
/// </summary>
[CreateAssetMenu(fileName = "New ResourceData", menuName = "Aetherbound/Data/Resource")]
public class ResourceData : ScriptableObject
{
    [Header("Resource Base Info")]
    public string resourceName;
    [TextArea(3, 5)]
    public string description;
    public Sprite resourceIcon;
    public ItemRarity rarity;
    public int resourceLevel;

    [Header("Stacking")]
    public bool isStackable = true;
    public int maxStackSize = 99;
}
