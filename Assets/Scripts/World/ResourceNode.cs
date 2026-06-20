using UnityEngine;

public class ResourceNode : Interactable
{
    public ResourceSourceData resourceSourceData;

    private int currentAmount;
    private bool isDepleted = false;
    private float respawnTimer = 0f;
    private GameObject originalVisual;

    void Start()
    {
        if (resourceSourceData == null)
        {
            Debug.LogError($"Resource Node on {gameObject.name} is missing ResourceSourceData!");
            this.enabled = false;
            return;
        }

        currentAmount = resourceSourceData.amountInNode;
        promptMessage = $"Gather {resourceSourceData.sourceName}";
        originalVisual = GetMainVisual(); 
    }

    void Update()
    {
        if (isDepleted)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= resourceSourceData.respawnTime)
            {
                Respawn();
            }
        }
    }

    protected override void Interact()
    {
        if (isDepleted) return;

        Debug.Log($"Gathering from {resourceSourceData.sourceName} node.");

        bool wasAdded = InventoryManager.Instance.AddItem(resourceSourceData.itemToGive, 1);

        if (wasAdded)
        {
            currentAmount--;
            Debug.Log($"Gave 1 {resourceSourceData.itemToGive.resourceName}. {currentAmount} remaining.");

            if (currentAmount <= 0)
            {
                Deplete();
            }
        }
        else
        {
            Debug.LogWarning("Inventory is full!");
        }
    }

    private void Deplete()
    {
        isDepleted = true;
        respawnTimer = 0f;
        Debug.Log($"{resourceSourceData.sourceName} node depleted.");

        if (resourceSourceData.depletedVisual != null)
        {
            if(originalVisual != null) originalVisual.SetActive(false);
            resourceSourceData.depletedVisual.SetActive(true);
        }
        else
        {
            GetComponent<Collider>().enabled = false;
        }

        gameObject.layer = LayerMask.NameToLayer("Default"); 
    }

    private void Respawn()
    {
        isDepleted = false;
        currentAmount = resourceSourceData.amountInNode;
        Debug.Log($"{resourceSourceData.sourceName} node has respawned.");

        if (resourceSourceData.depletedVisual != null)
        {
            if(originalVisual != null) originalVisual.SetActive(true);
            resourceSourceData.depletedVisual.SetActive(false);
        }
        else
        {
            GetComponent<Collider>().enabled = true;
        }

        gameObject.layer = LayerMask.NameToLayer("Interaction");
    }

    private GameObject GetMainVisual()
    {
        if(transform.childCount > 0) return transform.GetChild(0).gameObject;
        return null;
    }
}
