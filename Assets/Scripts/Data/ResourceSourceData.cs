using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Source", menuName = "Aetherbound/Data/Resource Source")]
public class ResourceSourceData : ScriptableObject
{
    [Header("Source Info")]
    public string sourceName;
    public ResourceData itemToGive;

    [Tooltip("The total number of items this node contains before being depleted.")]
    public int amountInNode = 5;

    [Tooltip("The time in seconds it takes for this node to respawn after being depleted.")]
    public float respawnTime = 30f;

    [Header("Visuals")]
    [Tooltip("The visual to display when the node is depleted (e.g., a tree stump). Optional.")]
    public GameObject depletedVisual;
}
