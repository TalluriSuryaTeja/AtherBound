using UnityEngine;

[CreateAssetMenu(fileName = "New ResourceData", menuName = "Aetherbound/Data/Resource")]
public class ResourceData : ItemData
{
    // All base fields (itemName, description, itemIcon, maxStackSize, etc.) 
    // are automatically inherited from the abstract ItemData class!
    
    // You can add resource-specific fields here later if you need them,
    // like "public ResourceType type;"
}
