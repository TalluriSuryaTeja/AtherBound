using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegenRate = 2f; // Mana regenerated per second

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
        }
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }
}