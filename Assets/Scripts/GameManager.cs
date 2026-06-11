using UnityEngine;
using System; // Added for Actions/Events

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Resources")]
    public int woodCount = 0;
    public int coalCount = 0;
    public int metalCount = 0;
    
    [Header("Magic/Energy")]
    public int aetherEnergy = 0; // The core magical energy

    // Event that triggers whenever any resource changes
    public event Action OnResourceUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddWood(int amount) { woodCount += amount; OnResourceUpdated?.Invoke(); }
    public void AddCoal(int amount) { coalCount += amount; OnResourceUpdated?.Invoke(); }
    public void AddMetal(int amount) { metalCount += amount; OnResourceUpdated?.Invoke(); }
    public void AddAether(int amount) { aetherEnergy += amount; OnResourceUpdated?.Invoke(); }

    public bool ConsumeResources(int wood, int coal, int metal)
    {
        if (woodCount >= wood && coalCount >= coal && metalCount >= metal)
        {
            woodCount -= wood;
            coalCount -= coal;
            metalCount -= metal;
            OnResourceUpdated?.Invoke(); // Trigger UI update
            return true;
        }
        return false;
    }
}
