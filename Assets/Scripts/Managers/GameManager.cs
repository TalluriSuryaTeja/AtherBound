using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Day/Night Cycle
    public float timeOfDay; // 0-1, 0 is midnight, 0.5 is noon
    public float dayDuration = 120f; // in seconds
    public event Action<float> onTimeChanged;

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
    }

    private void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime / dayDuration;
        timeOfDay %= 1; // loop time

        onTimeChanged?.Invoke(timeOfDay);
    }
}
