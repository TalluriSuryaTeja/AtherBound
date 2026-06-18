using UnityEngine;

public class UIInventory : MonoBehaviour
{
    // This script would be more complex, involving slots, item icons, etc.
    // For now, it's a placeholder to establish the UI script structure.

    // Example: A reference to a grid layout where inventory slots will be created
    public Transform inventorySlotGrid;
    public GameObject inventorySlotPrefab;

    private void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryChanged += UpdateInventoryUI;
        }
    }

    private void OnDisable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryChanged -= UpdateInventoryUI;
        }
    }

    void UpdateInventoryUI()
    {
        // Clear existing slots
        foreach (Transform child in inventorySlotGrid)
        {
            Destroy(child.gameObject);
        }

        // Create new slots based on the inventory
        if (InventoryManager.Instance != null)
        {
            foreach (var item in InventoryManager.Instance.items)
            {
                GameObject slot = Instantiate(inventorySlotPrefab, inventorySlotGrid);
                // Here you would set the icon, quantity text, etc. on the slot
            }
        }
    }
}
