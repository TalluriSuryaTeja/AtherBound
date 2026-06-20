using UnityEngine;

public class ResourceNode : Interactable
{
    public ItemData resourceItem; // The item to give (e.g., Wood Log)
    public int resourceAmount = 5; // How many items this node contains
    public float respawnTime = 30f; // Time in seconds to respawn
    public GameObject depletedVisual; // Optional: A different model to show when depleted (e.g., a stump)

    private int currentAmount;
    private bool isDepleted = false;
    private float respawnTimer = 0f;
    private GameObject originalVisual;

    void Start()
    {
        currentAmount = resourceAmount;
        promptMessage = $"Gather {resourceItem.itemName}";
        originalVisual = GetMainVisual(); // Assumes the main model is the first child
    }

    void Update()
    {
        if (isDepleted)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnTime)
            {
                Respawn();
            }
        }
    }

    protected override void Interact()
    {
        if (isDepleted) return;

        Debug.Log($"Gathering from {resourceItem.itemName} node.");

        // Try to add the item to the player's inventory
        bool wasAdded = InventoryManager.Instance.AddItem(resourceItem, 1);

        if (wasAdded)
        {
            currentAmount--;
            Debug.Log($"Gave 1 {resourceItem.itemName}. {currentAmount} remaining.");

            if (currentAmount <= 0)
            {
                Deplete();
            }
        }
        else
        {
            Debug.LogWarning("Inventory is full!");
            // Optionally, provide feedback to the player that their inventory is full
        }
    }

    private void Deplete()
    {
        isDepleted = true;
        respawnTimer = 0f;
        Debug.Log($"{resourceItem.itemName} node depleted.");

        // Switch to the depleted visual, if one is assigned
        if (depletedVisual != null)
        {
            if(originalVisual != null) originalVisual.SetActive(false);
            depletedVisual.SetActive(true);
        }
        else
        {
            // If no depleted visual, just disable the collider so it can't be interacted with
            GetComponent<Collider>().enabled = false;
        }

        // To hide the interaction prompt immediately
        gameObject.layer = LayerMask.NameToLayer("Default"); 
    }

    private void Respawn()
    {
        isDepleted = false;
        currentAmount = resourceAmount;
        Debug.Log($"{resourceItem.itemName} node has respawned.");

        if (depletedVisual != null)
        {
            if(originalVisual != null) originalVisual.SetActive(true);
            depletedVisual.SetActive(false);
        }
        else
        {
            GetComponent<Collider>().enabled = true;
        }

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }

    // Helper to get the main visual of the node
    private GameObject GetMainVisual()
    {
        if(transform.childCount > 0) return transform.GetChild(0).gameObject;
        return null;
    }
}
