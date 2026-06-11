using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public enum ResourceType { Wood, Coal, Metal, Aether }

    [Header("Resource Settings")]
    public ResourceType resourceType;
    public int amountPerGather = 1;
    public int totalResource = 5;

    public void Gather(PlayerController.PlayerID playerID)
    {
        if (totalResource <= 0) return;

        int amountToGive = Mathf.Min(amountPerGather, totalResource);
        totalResource -= amountToGive;

        // Add to GameManager based on type
        switch (resourceType)
        {
            case ResourceType.Wood:
                GameManager.Instance.AddWood(amountToGive);
                break;
            case ResourceType.Coal:
                GameManager.Instance.AddCoal(amountToGive);
                break;
            case ResourceType.Metal:
                GameManager.Instance.AddMetal(amountToGive);
                break;
            case ResourceType.Aether:
                GameManager.Instance.AddAether(amountToGive);
                break;
        }

        Debug.Log($"{playerID} gathered {amountToGive} {resourceType}. Remaining: {totalResource}");

        // Destroy the node when it runs out of resources
        if (totalResource <= 0)
        {
            Debug.Log($"{resourceType} node depleted!");
            Destroy(gameObject); // Optional: Play a particle effect before destroying
        }
    }
}
