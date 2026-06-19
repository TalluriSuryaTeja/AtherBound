using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum TimeOfDay
    {
        Day,
        Night
    }

    public static event Action<TimeOfDay> OnTimeOfDayChanged;
    public static event Action OnResourceUpdated;
    public event Action<float> onTimeChanged;


    [Header("Day/Night Cycle")]
    [Range(0, 1)]
    public float timeOfDay; // 0-1, 0 is midnight, 0.5 is noon
    public float dayDuration = 120f; // in seconds

    [Header("Player Resources")]
    public int woodCount = 0;
    public int coalCount = 0;
    public int metalCount = 0;
    public int aetherEnergy = 0;

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

        UpdateTimeOfDay(true);
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime / dayDuration;
        timeOfDay %= 1; // loop time

        onTimeChanged?.Invoke(timeOfDay);

        UpdateTimeOfDay(false);
    }

    private void UpdateTimeOfDay(bool forceUpdate)
    {
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

    public void AddResource(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "Wood":
                woodCount += amount;
                break;
            case "Coal":
                coalCount += amount;
                break;
            case "Metal":
                metalCount += amount;
                break;
            case "Aether":
                aetherEnergy += amount;
                break;
        }
        OnResourceUpdated?.Invoke();
    }
}
