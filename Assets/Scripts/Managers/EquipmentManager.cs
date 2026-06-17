using UnityEngine;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    [Header("Equipped Items")]
    public Dictionary<EquipmentSlot, EquipmentData> equippedItems = new Dictionary<EquipmentSlot, EquipmentData>();

    private PlayerStats playerStats;
    private InventoryManager inventoryManager;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        inventoryManager = GetComponent<InventoryManager>();

        foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
        {
            if (!equippedItems.ContainsKey(slot))
            {
                equippedItems.Add(slot, null);
            }
        }
    }

    /// <summary>
    /// The main public method for equipping an item. It handles unequipping the old item.
    /// </summary>
    public void EquipItem(EquipmentData newItem)
    {
        if (newItem == null) return;

        EquipmentSlot slot = newItem.equipmentSlot;

        // Check if there's already an item in the slot
        if (equippedItems.TryGetValue(slot, out EquipmentData oldItem) && oldItem != null)
        {
            // Unequip the old item and add it back to the inventory
            inventoryManager.AddItem(oldItem);
        }

        // Equip the new item
        equippedItems[slot] = newItem;

        // Remove the new item from the inventory
        inventoryManager.RemoveItem(newItem);

        // Crucially, notify PlayerStats to recalculate everything
        playerStats.RecalculateAllStats();

        Debug.Log($"Equipped {newItem.itemName} to {slot}. Stats recalculated.");
    }

    /// <summary>
    /// Unequips an item from a specific slot and adds it back to the inventory.
    /// </summary>
    public void UnequipItem(EquipmentSlot slot)
    {
        if (equippedItems.TryGetValue(slot, out EquipmentData itemToUnequip) && itemToUnequip != null)
        {
            // Add the item back to the inventory
            inventoryManager.AddItem(itemToUnequip);

            // Remove the item from the equipped slot
            equippedItems[slot] = null;

            // Recalculate stats
            playerStats.RecalculateAllStats();

            Debug.Log($"Unequipped {itemToUnequip.itemName} from {slot}. Stats recalculated.");
        } 
    }

    /// <summary>
    /// Gathers all stat bonuses from every equipped item.
    /// </summary>
    public List<StatModifier> GetAllEquippedStatBonuses()
    {
        List<StatModifier> totalBonuses = new List<StatModifier>();
        foreach (EquipmentData item in equippedItems.Values)
        {
            if (item != null && item.statModifiers != null)
            {
                totalBonuses.AddRange(item.statModifiers);
            }
        }
        return totalBonuses;
    }
}
