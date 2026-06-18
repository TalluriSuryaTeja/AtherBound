using UnityEngine;
using TMPro;

public class UICharacterProfile : MonoBehaviour
{
    // References to UI elements for displaying character stats
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI raceText;
    public TextMeshProUGUI classText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI dexterityText;
    public TextMeshProUGUI intelligenceText;

    private void OnEnable()
    {
        // Subscribe to events that notify of changes in character data
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onStatsChanged += UpdateCharacterProfile;
            UpdateCharacterProfile(); // Initial update
        }
    }

    private void OnDisable()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onStatsChanged -= UpdateCharacterProfile;
        }
    }

    void UpdateCharacterProfile()
    {
        if (PlayerStats.Instance == null) return;

        // Update all the text elements with the latest stats
        // Note: You\'ll need to implement the actual data retrieval from your managers
        // For example, PlayerStats.Instance.GetStat(Stat.Strength), etc.

        // nameText.text = "Player Name"; // Placeholder
        // raceText.text = "Race"; // Placeholder
        // classText.text = "Class"; // Placeholder
        // levelText.text = "Level: 1"; // Placeholder
        // strengthText.text = $"Strength: {PlayerStats.Instance.strength.GetValue()}";
        // dexterityText.text = $"Dexterity: {PlayerStats.Instance.dexterity.GetValue()}";
        // intelligenceText.text = $"Intelligence: {PlayerStats.Instance.intelligence.GetValue()}";
    }
}
