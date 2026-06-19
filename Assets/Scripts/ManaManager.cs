using UnityEngine;
using System;

public class ManaManager : MonoBehaviour
{
    public static ManaManager Instance { get; private set; }

    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 2f; // Mana regenerated per second

    public event Action<float, float> onManaChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        currentMana = maxMana;
    }

    void Update()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            onManaChanged?.Invoke(currentMana, maxMana);
        }
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            onManaChanged?.Invoke(currentMana, maxMana);
            return true;
        }
        return false;
    }
}