using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Enum for the time of day
    public enum TimeOfDay
    {
        Day,
        Night
    }

    // Event to broadcast when the time of day changes
    public static event Action<TimeOfDay> OnTimeOfDayChanged;

    [Header("Day/Night Cycle")]
    [Range(0, 1)]
    public float timeOfDay; // 0-1, 0 is midnight, 0.5 is noon
    public float dayDuration = 120f; // in seconds

    private TimeOfDay currentTimeOfDay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Initialize time of day
        UpdateTimeOfDay(true);
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;
        timeOfDay %= 1; // loop time

        UpdateTimeOfDay(false);
    }

    private void UpdateTimeOfDay(bool forceUpdate)
    {
        // Define day and night thresholds
        // Let's say night is from 7 PM (0.8125) to 6 AM (0.25)
        TimeOfDay newTimeOfDay = (timeOfDay > 0.25f && timeOfDay < 0.8125f) ? TimeOfDay.Day : TimeOfDay.Night;

        if (newTimeOfDay != currentTimeOfDay || forceUpdate)
        {
            currentTimeOfDay = newTimeOfDay;
            OnTimeOfDayChanged?.Invoke(currentTimeOfDay);
            Debug.Log("Time of day changed to: " + currentTimeOfDay);
        }
    }

    public TimeOfDay GetCurrentTimeOfDay()
    {
        return currentTimeOfDay;
    }
}
