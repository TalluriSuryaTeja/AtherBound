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
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResourceUpdated += UpdateResourceUI;
            UpdateResourceUI(); // Do an initial update to show starting values (zeros)
        }
    }

    private void OnDestroy()
    {
        // Always unsubscribe from events when the object is destroyed to prevent memory leaks
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResourceUpdated -= UpdateResourceUI;
        }
    }

    // This gets called automatically whenever GameManager changes a resource
    private void UpdateResourceUI()
    {
        if (woodText != null) woodText.text = "Wood: " + GameManager.Instance.woodCount;
        if (coalText != null) coalText.text = "Coal: " + GameManager.Instance.coalCount;
        if (metalText != null) metalText.text = "Metal: " + GameManager.Instance.metalCount;
        if (aetherText != null) aetherText.text = "Aether: " + GameManager.Instance.aetherEnergy;
    }
}
