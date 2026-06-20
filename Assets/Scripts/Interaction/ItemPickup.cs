using UnityEngine;

public class ItemPickup : Interactable
{
    public ItemData item;

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
