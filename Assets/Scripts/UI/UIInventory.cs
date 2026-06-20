using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public Transform inventorySlotGrid;
    public GameObject inventorySlotPrefab;

    void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.onInventoryChanged += UpdateInventoryUI;
            UpdateInventoryUI(); // Initial update
        }
    }

    void OnDisable()
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
            for (int i = 0; i < InventoryManager.Instance.items.Count; i++)
            {
                InventorySlot itemSlot = InventoryManager.Instance.items[i];
                GameObject slotGO = Instantiate(inventorySlotPrefab, inventorySlotGrid);

                // Set the slot index
                UISlot uiSlot = slotGO.GetComponent<UISlot>();
                if (uiSlot != null) uiSlot.slotIndex = i;

                // Here you would set the icon, quantity text, etc. on the slot

                // Find the Drop button and add a listener
                Button dropButton = slotGO.GetComponentInChildren<Button>(); // Assumes a button is a child of the slot
                if (dropButton != null)
                {
                    int index = i; // Capture the index for the closure
                    dropButton.onClick.AddListener(() => DropItem(index));
                }
            }
        }
    }

    void DropItem(int slotIndex)
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.DropItem(slotIndex, 1);
        }
    }
}
