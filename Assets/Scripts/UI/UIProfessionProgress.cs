using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIProfessionProgress : MonoBehaviour
{
    public ProfessionData professionToDisplay;
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI professionNameText;

    private void OnEnable()
    {
        if (ProfessionManager.Instance != null)
        {
            ProfessionManager.Instance.onExperienceChanged += UpdateProfessionUI;
            ProfessionManager.Instance.onLevelUp += UpdateLevel;

            // Set initial values
            UpdateProfessionUI(professionToDisplay, ProfessionManager.Instance.GetExperience(professionToDisplay), ProfessionManager.Instance.GetXPForNextLevel(professionToDisplay));
            UpdateLevel(professionToDisplay, ProfessionManager.Instance.GetLevel(professionToDisplay));
        }

        if(professionNameText != null && professionToDisplay != null)
        {
            professionNameText.text = professionToDisplay.professionName;
        }
    }

    private void OnDisable()
    {
        if (ProfessionManager.Instance != null)
        {
            ProfessionManager.Instance.onExperienceChanged -= UpdateProfessionUI;
            ProfessionManager.Instance.onLevelUp -= UpdateLevel;
        }
    }

    private void UpdateProfessionUI(ProfessionData profession, int currentXP, int requiredXP)
    {
        if (profession == professionToDisplay)
        {
            if (requiredXP > 0)
            {
                xpSlider.value = (float)currentXP / requiredXP;
            }
            else
            {
                xpSlider.value = 0;
            }
        }
    }

    private void UpdateLevel(ProfessionData profession, int newLevel)
    {
        if (profession == professionToDisplay)
        {
            levelText.text = $"Level: {newLevel}";
        }
    }
}
