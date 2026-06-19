using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public AetherboundInputActions InputActions { get; private set; }

    public event Action<string, string> OnRebindComplete;
    public event Action<string> OnRebindCancel;
    public event Action<string, string> OnRebindStart;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InputActions = new AetherboundInputActions();
        InputActions.Enable();

        LoadOverrides();
    }

    private void OnDestroy()
    {
        InputActions?.Dispose();
    }

    public void StartRebinding(string actionName, int bindingIndex, string deviceLayout)
    {
        InputAction action = InputActions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            Debug.LogError($"Could not find action '{actionName}' or binding index '{bindingIndex}'.");
            return;
        }

        var binding = action.bindings[bindingIndex];
        string controlScheme = deviceLayout.Contains("Keyboard") ? "KeyboardMouse" : "Gamepad";
        if (binding.groups.Split(';').All(g => g != controlScheme))
        {
            Debug.LogWarning($"The binding at index {bindingIndex} for action {actionName} does not belong to the {controlScheme} scheme. This may not be the intended binding to change.");
        }

        OnRebindStart?.Invoke(actionName, GetBindingDisplayName(actionName, bindingIndex));

        rebindingOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                OnRebindComplete?.Invoke(actionName, GetBindingDisplayName(actionName, bindingIndex));
                SaveBindingOverride(action);
            })
            .OnCancel(operation =>
            {
                operation.Dispose();
                OnRebindCancel?.Invoke(actionName);
            })
            .Start();
    }

    public string GetBindingDisplayName(string actionName, int bindingIndex)
    {
        InputAction action = InputActions.asset.FindAction(actionName);
        if (action == null || action.bindings.Count <= bindingIndex)
        {
            return "N/A";
        }
        return action.bindings[bindingIndex].ToDisplayString();
    }

    public void SaveBindingOverride(InputAction action)
    {
        for (int i = 0; i < action.bindings.Count; i++)
        {
            PlayerPrefs.SetString(action.actionMap + action.name + i, action.bindings[i].overridePath);
        }
        PlayerPrefs.Save();
    }

    public void LoadOverrides()
    {
        foreach (var map in InputActions.asset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    string overridePath = PlayerPrefs.GetString(action.actionMap + action.name + i);
                    if (!string.IsNullOrEmpty(overridePath))
                    {
                        action.ApplyBindingOverride(i, overridePath);
                    }
                }
            }
        }
    }

    public void ResetAllBindings()
    {
        foreach (var map in InputActions.asset.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        foreach (var map in InputActions.asset.actionMaps)
        {
            foreach (var action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    PlayerPrefs.DeleteKey(action.actionMap + action.name + i);
                }
            }
        }
        PlayerPrefs.Save();
    }
}
