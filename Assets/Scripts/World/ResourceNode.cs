using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ResourceNode : MonoBehaviour
{
    [Header("Resource Settings")]
    public ItemData itemToYield;
    public int quantity = 1;
    public float gatheringTime = 2.0f;
    public float respawnTime = 60.0f;

    [Header("Profession & Experience")]
    [Tooltip("The profession associated with this node (e.g., Mining, Herbalism).")]
    public ProfessionData associatedProfession;
    [Tooltip("The amount of XP granted in the associated profession upon a successful gather.")]
    public float xpValue = 10f;

    [Header("Visuals")]
    [Tooltip("The main visual object of the node. This will be disabled when depleted.")]
    public GameObject resourceVisual;

    private bool isDepleted = false;

    void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public bool IsDepleted()
    {
        return isDepleted;
    }

    public void StartGathering()
    {
        if (isDepleted || itemToYield == null)
        {
            return;
        }

        StartCoroutine(GatherCoroutine());
    }

    private IEnumerator GatherCoroutine()
    {
        Debug.Log($"Gathering {itemToYield.name}...");
        yield return new WaitForSeconds(gatheringTime);

        DepleteNode();

        // Grant XP to the player
        if (associatedProfession != null && ProfessionManager.Instance != null)
        {
            ProfessionManager.Instance.AddExperience(associatedProfession, xpValue);
        }

        // Add the item to the player's inventory
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.AddItem(itemToYield, quantity);
            Debug.Log($"Gathered {quantity}x {itemToYield.name}! Gained {xpValue} {associatedProfession.professionName} XP.");
        }
        else
        {
            Debug.LogWarning("InventoryManager not found. Could not add item.");
        }
    }

    private void DepleteNode()
    {
        isDepleted = true;

        if (resourceVisual != null)
        {
            resourceVisual.SetActive(false);
        }

        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnTime);
        RespawnNode();
    }

    private void RespawnNode()
    {
        isDepleted = false;

        if (resourceVisual != null)
        {
            resourceVisual.SetActive(true);
        }

        Debug.Log("A resource node has respawned.");
    }
}
