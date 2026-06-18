using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UIManaBar : MonoBehaviour
{
    private Slider manaSlider;

    private void Awake()
    {
        manaSlider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        // Assuming a ManaManager exists and is accessible.
        // You might need to find it or have it passed in.
        if (ManaManager.Instance != null)
        {
            ManaManager.Instance.onManaChanged += UpdateManaBar;
            // Set initial value
            UpdateManaBar(ManaManager.Instance.currentMana, ManaManager.Instance.maxMana);
        }
    }

    private void OnDisable()
    {
        if (ManaManager.Instance != null)
        {
            ManaManager.Instance.onManaChanged -= UpdateManaBar;
        }
    }

    private void UpdateManaBar(float currentMana, float maxMana)
    {
        if (maxMana > 0)
        {
            manaSlider.value = currentMana / maxMana;
        }
    }
}
