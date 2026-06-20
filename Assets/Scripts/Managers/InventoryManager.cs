using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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

    public event Action onInventoryChanged;

    [Header("Inventory Settings")]
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int inventorySize = 20;

    [Header("Item Dropping")]
    public GameObject itemPickupPrefab; // Assign in editor
    public Transform playerTransform; // Assign player's transform in editor

    public List<InventorySlot> items => inventorySlots;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public bool AddItem(ItemData item, int quantity = 1)
    {
        if (item == null || quantity <= 0) return false;
        
        int initialQuantity = quantity;

        if (item.isStackable)
        {
            InventorySlot existingSlot = inventorySlots.FirstOrDefault(slot => 
                slot.item == item && slot.quantity < item.maxStackSize);

            if (existingSlot != null)
            {
                int spaceAvailable = item.maxStackSize - existingSlot.quantity;
                int amountToAdd = Mathf.Min(quantity, spaceAvailable);
                
                existingSlot.AddQuantity(amountToAdd);
                quantity -= amountToAdd;
            }

            while (quantity > 0 && inventorySlots.Count < inventorySize)
            {
                int amountForNewStack = Mathf.Min(quantity, item.maxStackSize);
                inventorySlots.Add(new InventorySlot(item, amountForNewStack));
                quantity -= amountForNewStack;
            }
        }
        else
        {
            while (quantity > 0 && inventorySlots.Count < inventorySize)
            {
                inventorySlots.Add(new InventorySlot(item, 1));
                quantity--;
            }
        }

        bool itemsWereAdded = initialQuantity > quantity;
        if(itemsWereAdded)
        {
            Debug.Log($"Added {initialQuantity - quantity} of {item.itemName} to inventory.");
            onInventoryChanged?.Invoke();
        }

        if (quantity > 0)
        {
            Debug.LogWarning($"Inventory is full. Could not add {quantity} of {item.itemName}.");
        }
        
        return quantity == 0;
    }

    public void RemoveItem(ItemData item, int quantity = 1)
    {
        if (item == null || quantity <= 0) return;

        int initialQuantity = quantity;

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

        bool itemsWereRemoved = initialQuantity > quantity;
        if(itemsWereRemoved)
        {
            onInventoryChanged?.Invoke();
        }

        if (quantity > 0)
        {
            Debug.LogWarning($"Could not find enough {item.itemName} to remove.");
        }
    }

    public void RemoveFromSlot(int slotIndex, int quantity = 1)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Count) return;

        InventorySlot slot = inventorySlots[slotIndex];
        slot.RemoveQuantity(quantity);

        if (slot.quantity <= 0)
        {
            inventorySlots.RemoveAt(slotIndex);
        }
        onInventoryChanged?.Invoke();
    }

    public void DropItem(int slotIndex, int quantity = 1)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Count) return;
        if (itemPickupPrefab == null || playerTransform == null)
        {
            Debug.LogError("Item Drop Prefab or Player Transform not assigned in InventoryManager.");
            return;
        }

        InventorySlot slot = inventorySlots[slotIndex];
        ItemData itemToDrop = slot.item;

        // Calculate drop position in front of the player
        Vector3 dropPosition = playerTransform.position + playerTransform.forward * 1.5f + Vector3.up * 0.5f;

        // Instantiate the item pickup in the world
        GameObject droppedItemObject = Instantiate(itemPickupPrefab, dropPosition, Quaternion.identity);
        ItemPickup pickup = droppedItemObject.GetComponent<ItemPickup>();
        if(pickup != null)
        {
            pickup.item = itemToDrop;
        }
        else
        {
            Debug.LogError("Assigned Item Drop Prefab does not have an ItemPickup component!");
        }

        // Remove the item from inventory
        RemoveFromSlot(slotIndex, quantity);
    }
}
