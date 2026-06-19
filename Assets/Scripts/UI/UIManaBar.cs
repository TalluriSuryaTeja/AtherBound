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
        if (ManaManager.Instance != null)
        {
            ManaManager.Instance.onManaChanged += UpdateManaBar;
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
