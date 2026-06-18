using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuInputUI : MonoBehaviour
{
    [System.Serializable]
    public class RebindableUIElements
    {
        public string actionName;
        public TextMeshProUGUI bindingDisplayText;
        public Button rebindButton;
        public int bindingIndex; // 0 for primary, 1 for secondary, etc.
        public string deviceLayout; // e.g., "Keyboard", "Gamepad"
    }

    public RebindableUIElements[] rebindableElements;
    public Button resetButton;

    private void Start()
    {
        if (InputManager.Instance == null)
        {
            Debug.LogError("InputManager not found in the scene!");
            this.enabled = false;
            return;
        }

        // Setup button listeners
        foreach (var element in rebindableElements)
        {
            element.rebindButton.onClick.AddListener(() => StartRebindingProcess(element));
        }
        resetButton.onClick.AddListener(ResetAllBindings);

        // Subscribe to events
        InputManager.Instance.OnRebindComplete += UpdateButtonText;
        InputManager.Instance.OnRebindCancel += OnRebindCancelled;
        InputManager.Instance.OnRebindStart += OnRebindStarted;

        // Initial population of text
        UpdateAllButtonTexts();
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnRebindComplete -= UpdateButtonText;
            InputManager.Instance.OnRebindCancel -= OnRebindCancelled;
            InputManager.Instance.OnRebindStart -= OnRebindStarted;
        }
    }

    private void UpdateAllButtonTexts()
    {
        foreach (var element in rebindableElements)
        {
            UpdateButtonText(element.actionName, InputManager.Instance.GetBindingDisplayName(element.actionName, element.bindingIndex));
        }
    }

    private void StartRebindingProcess(RebindableUIElements element)
    {
        InputManager.Instance.StartRebinding(element.actionName, element.bindingIndex, element.deviceLayout);
    }

    private void OnRebindStarted(string actionName, string currentBinding)
    {
        foreach (var element in rebindableElements)
        {
            if (element.actionName == actionName)
            {
                element.bindingDisplayText.text = "Listening...";
            }
        }
    }

    private void OnRebindCancelled(string actionName)
    {
        // Simply revert to the current binding text
        UpdateAllButtonTexts();
    }

    private void UpdateButtonText(string actionName, string newBindingDisplayName)
    {
        foreach (var element in rebindableElements)
        {
            if (element.actionName == actionName)
            {
                 element.bindingDisplayText.text = newBindingDisplayName;
            }
        }
    }

    private void ResetAllBindings()
    {
        InputManager.Instance.ResetAllBindings();
        UpdateAllButtonTexts();
    }
}
