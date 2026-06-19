using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a single slot in the inventory.
/// </summary>
[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int quantity;

    public InventorySlot(ItemData item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }

    public void RemoveQuantity(int amount)
    {
        quantity -= amount;
    }
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("Inventory")]
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int inventorySize = 20;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    /// <summary>
    /// Adds an item (or a stack of items) to the inventory.
    /// </summary>
    /// <returns>True if the item was added successfully, false if the inventory is full.</returns>
    public bool AddItem(ItemData item, int quantity = 1)
    {
        if (item == null || quantity <= 0) return false;

        // Handle stackable items
        if (item.isStackable)
        {
            // 1. Try to find an existing stack with space
            InventorySlot existingSlot = inventorySlots.FirstOrDefault(slot => 
                slot.item == item && slot.quantity < item.maxStackSize);

            if (existingSlot != null)
            {
                int spaceAvailable = item.maxStackSize - existingSlot.quantity;
                int amountToAdd = Mathf.Min(quantity, spaceAvailable);
                
                existingSlot.AddQuantity(amountToAdd);
                quantity -= amountToAdd;
            }

            // 2. If there's still quantity left, find new slots for new stacks
            while (quantity > 0 && inventorySlots.Count < inventorySize)
            {
                int amountForNewStack = Mathf.Min(quantity, item.maxStackSize);
                inventorySlots.Add(new InventorySlot(item, amountForNewStack));
                quantity -= amountForNewStack;
            }
        }
        // Handle non-stackable items
        else
        {
            while (quantity > 0 && inventorySlots.Count < inventorySize)
            {
                inventorySlots.Add(new InventorySlot(item, 1));
                quantity--;
            }
        }

        if (quantity > 0)
        {
            Debug.LogWarning($"Inventory is full. Could not add {quantity} of {item.itemName}.");
            return false;
        }

        Debug.Log($"Added {item.itemName} to inventory.");
        return true;
    }

    /// <summary>
    /// Removes an item (or a stack) from the inventory.
    /// </summary>
    public void RemoveItem(ItemData item, int quantity = 1)
    {
        if (item == null || quantity <= 0) return;

        for (int i = inventorySlots.Count - 1; i >= 0; i--)
        {
            InventorySlot slot = inventorySlots[i];
            if (slot.item == item)
            {
                int amountToRemove = Mathf.Min(quantity, slot.quantity);
                slot.RemoveQuantity(amountToRemove);
                quantity -= amountToRemove;

                if (slot.quantity <= 0)
                {
                    inventorySlots.RemoveAt(i);
                }

                if (quantity <= 0) break; 
            }
        }

        if (quantity > 0)
        {
            Debug.LogWarning($"Could not find enough {item.itemName} to remove.");
        }
    }
}
