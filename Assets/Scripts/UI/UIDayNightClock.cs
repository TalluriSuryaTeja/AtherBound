using UnityEngine;
using TMPro;

public class UIDayNightClock : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onTimeChanged += UpdateClock;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onTimeChanged -= UpdateClock;
        }
    }

    private void UpdateClock(float time)
    {
        // Format the time to a more readable HH:MM format
        int hours = (int)(time * 24);
        int minutes = (int)((time * 24 * 60) % 60);
        timeText.text = $"{hours:00}:{minutes:00}";
    }
}
