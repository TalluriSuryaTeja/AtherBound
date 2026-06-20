using UnityEngine;

public class ItemPickup : Interactable, IItemPickup
{
    public ItemData item;
    public ItemData Item { get => item; set => item = value; }

    private void Start()
    {
        if(string.IsNullOrEmpty(promptMessage))
        {
            promptMessage = $"Pick up {item.name}";
        }
    }

    protected override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        Debug.Log($"Picking up {item.name}");
        bool wasPickedUp = InventoryManager.Instance.AddItem(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
