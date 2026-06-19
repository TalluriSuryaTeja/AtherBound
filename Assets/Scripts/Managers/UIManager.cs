using UnityEngine;
using TMPro; // Unity's modern text system

public class UIManager : MonoBehaviour
{
    [Header("Resource Texts")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI coalText;
    public TextMeshProUGUI metalText;
    public TextMeshProUGUI aetherText;

    private void Start()
    {
        // Subscribe to the GameManager's update event
        // OnResourceUpdated is a static event, so we subscribe via the class name
        GameManager.OnResourceUpdated += UpdateResourceUI;
        // It's still good practice to check if the instance exists before trying to get initial values
        if (GameManager.Instance != null)
        {
            UpdateResourceUI(); // Do an initial update to show starting values
        }
    }

    private void OnDestroy()
    {
        // Always unsubscribe from events when the object is destroyed
        // OnResourceUpdated is a static event, so we unsubscribe via the class name
        GameManager.OnResourceUpdated -= UpdateResourceUI;
    }

    // This gets called automatically whenever GameManager changes a resource
    private void UpdateResourceUI()
    {
        // We still need the instance to get the current resource counts
        if (GameManager.Instance != null)
        {
            if (woodText != null) woodText.text = "Wood: " + GameManager.Instance.woodCount;
            if (coalText != null) coalText.text = "Coal: " + GameManager.Instance.coalCount;
            if (metalText != null) metalText.text = "Metal: " + GameManager.Instance.metalCount;
            if (aetherText != null) aetherText.text = "Aether: " + GameManager.Instance.aetherEnergy;
        }
    }
}
