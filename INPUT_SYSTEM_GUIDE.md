# AetherBound: Input System & Controls Guide

This guide details how to set up the player input system, including local co-op and a remappable control scheme.

---

## Part 1: Creating the Input Actions Asset

1.  **Create the Asset:** In the **Project** window, right-click and go to **Create > Input Actions**. Name the new asset `PlayerInputActions`.
2.  **Enable Auto-Generation:** Select the `PlayerInputActions` asset. In the Inspector, check the box for **"Generate C# Class"** and click **"Apply"**. This will create the `PlayerInputActions.cs` script that our `InputManager` uses.
3.  **Define Action Maps:**
    *   Double-click the `PlayerInputActions` asset to open the Action Editor.
    *   Click the **"+"** under **Action Maps** to create a new map. Name it `Player`.
4.  **Define Actions & Bindings:**
    *   In the `Player` action map, create the following actions:
        *   `Move` (Action Type: Value, Control Type: Vector 2)
        *   `Look` (Action Type: Value, Control Type: Vector 2)
        *   `Jump` (Action Type: Button)
        *   `Sprint` (Action Type: Button)
        *   `Magic` (Action Type: Button)
    *   **For each action, add two bindings:** one for Keyboard/Mouse and one for Gamepad.
        *   **Move (Keyboard):** Add a `WASD Vector2` composite binding.
        *   **Move (Gamepad):** Bind to `Left Stick [Gamepad]`.
        *   **Look (Keyboard):** Bind to `Delta [Mouse]`.
        *   **Look (Gamepad):** Bind to `Right Stick [Gamepad]`.
        *   **Jump (Keyboard):** Bind to `Space`.
        *   **Jump (Gamepad):** Bind to `Button South [Gamepad]` (e.g., A/X).
        *   **Sprint (Keyboard):** Bind to `Left Shift`.
        *   **Sprint (Gamepad):** Bind to `Left Trigger [Gamepad]`.
        *   **Magic (Keyboard):** Bind to `E`.
        *   **Magic (Gamepad):** Bind to `Button East [Gamepad]` (e.g., B/Circle).
5.  **Save the Asset:** Click **"Save Asset"** in the Action Editor window.

---

## Part 2: Setting up the InputManager Singleton

1.  **Create an Empty GameObject:** In your first scene (e.g., a Main Menu or initialization scene), create an empty GameObject and name it `InputManager`.
2.  **Add the Script:** Add the `InputManager.cs` script to this GameObject.
3.  **Make it Persistent:** The script already handles the Singleton pattern (`DontDestroyOnLoad`), so no further action is needed. It will persist across all scenes.

---

## Part 3: Creating a Rebindable Controls Menu

This assumes you have a UI Canvas with a panel for your settings menu.

1.  **Create the UI Elements:** For each action you want to be rebindable, create a set of UI objects:
    *   A **TextMeshPro Text** label (e.g., "Move Forward")
    *   A **Button** with a **TextMeshPro Text** child. The child text will display the current binding (e.g., "W").

    *Example for one Keyboard binding:*
    `[Label: "Jump"]` `[Button: [Text: "Space"]]`

2.  **Add the UI Controller Script:**
    *   Select the main panel for your controls menu.
    *   Add the `SettingsMenuInputUI.cs` script to it.

3.  **Configure the `SettingsMenuInputUI` Component:**
    *   In the Inspector, lock the `SettingsMenuInputUI` component to make dragging references easier.
    *   Expand the `Rebindable Elements` array.
    *   For each rebindable action, create an element in this array.
    *   **Drag and drop** the corresponding UI Button and Text objects into the fields.
    *   **Action Name:** Type the exact name of the action from your Input Actions asset (e.g., "Jump").
    *   **Binding Index:** Use `0` for the primary binding (e.g., Keyboard) and `1` for the secondary (e.g., Gamepad).
    *   **Device Layout:** Type `Keyboard` for keyboard bindings and `Gamepad` for gamepad bindings. This is used to ensure you are modifying the correct binding.

4.  **Reset Button:**
    *   Create one more button on your panel, labeled "Reset to Defaults".
    *   Drag this button into the **Reset Button** field in the `SettingsMenuInputUI` component.

5.  **Run and Test:** When you run the scene, the text fields should automatically populate with the default bindings. Clicking a rebind button will prompt for a new input, and the UI and underlying controls will update automatically.