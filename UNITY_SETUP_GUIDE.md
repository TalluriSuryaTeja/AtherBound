# AetherBound: Manual Unity Setup Guide

This guide provides step-by-step instructions to set up the game's backend and UI manually within the Unity Editor. Follow the steps in order. When you encounter a **CODE CHANGE REQUIRED** step, notify the Gemini assistant to perform the code update for you.

---

## Part 1: Setting Up The Backend (No Code Changes)

**Goal:** To configure your player character and create the necessary game data assets.

### Step 1.1: Add Manager Scripts to the Player
1.  In the **Hierarchy** window, find and select your player object (e.g., `PlayerArmature`).
2.  With the player selected, go to the **Inspector** window.
3.  Click the **"Add Component"** button.
4.  Search for and add each of the following scripts, one by one:
    *   `PlayerStats`
    *   `ClassManager`
    *   `EquipmentManager`
    *   `InventoryManager`

### Step 1.2: Create Game Data Folders
1.  In the **Project** window, navigate to the `Assets` folder.
2.  Right-click and select **Create > Folder**.
3.  Name the new folder `GameData`.
4.  Inside the `GameData` folder, create subfolders for `Races`, `Classes`, and `Items`.

### Step 1.3: Create Race, Class, and Item Data
1.  Navigate into the `Assets/GameData/Races` folder.
2.  Right-click, then go to **Create > AetherBound > Race**. Name it something like `Human_Race`.
3.  Select the new asset and fill in its base stats in the Inspector.
4.  Navigate into the `Assets/GameData/Classes` folder.
5.  Right-click, then go to **Create > AetherBound > Class**. Name it `Warrior_Class`.
6.  Select the new asset and configure its level-up stats in the Inspector.
7.  Navigate into the `Assets/GameData/Items` folder.
8.  Right-click, then go to **Create > AetherBound > Equipment**. Name it `Iron_Sword`.
9.  Select the new asset and, in the Inspector, add a Stat Modifier (e.g., `Strength`, `Flat`, `5`).

### Step 1.4: Link Data to the Player
1.  Select your player object in the Hierarchy again.
2.  In the Inspector, find the **Player Stats** component.
3.  Drag your `Human_Race` asset from the Project window into the **Base Race** field.
4.  Find the **Class Manager** component.
5.  Drag your `Warrior_Class` asset into the **Current Class** field.

---

## Part 2: Preparing for the UI (Code Change Required)

**Goal:** To modify the backend scripts so they can send signals (events) when data changes. The UI will need to listen for these signals.

### Step 2.1: **CODE CHANGE REQUIRED: Add Events to Manager Scripts**
*   **Goal:** To allow `PlayerStats`, `InventoryManager`, and `ClassManager` to broadcast updates.
*   **Files to be Modified:** `PlayerStats.cs`, `InventoryManager.cs`, `ClassManager.cs`.
*   **Action:** When you are ready for this change, tell the Gemini assistant: **"I am at Step 2.1, please update the manager scripts."**

---

## Part 3: Building the UI Elements (No Code Changes)

**Goal:** To create the visual elements for your UI inside the Unity Editor.

### Step 3.1: Create the Character Profile UI
1.  In the Hierarchy, right-click and select **UI > Panel**. This will create a Canvas, a Panel, and an EventSystem.
2.  Rename the new Panel to `CharacterProfilePanel`.
3.  Select the `CharacterProfilePanel`.
4.  Right-click it and select **UI > Text - TextMeshPro** to create a text label. You may be prompted to import TMP Essentials; click to do so.
5.  Rename the new text object `Strength_Text`.
6.  In its Inspector, change the **Text** field to "Strength: 0".
7.  Duplicate this text object for other stats like Dexterity, Intelligence, etc.

### Step 3.2: Create the Experience Bar UI
1.  In the Hierarchy, right-click the Canvas and select **UI > Slider**.
2.  Rename the new Slider to `Experience_Slider`.
3.  Select the `Experience_Slider`. In the Inspector, set the **Value** to 0.
4.  Right-click the Canvas again and select **UI > Text - TextMeshPro**.
5.  Rename the new text object `Level_Text`. Set its text to "Level: 1".

---

## Part 4: Connecting the UI (Code Change Required)

**Goal:** To create new scripts that will act as the "glue" between your UI elements and the backend, and then connect them in the editor.

### Step 4.1: **CODE CHANGE REQUIRED: Create UI Controller Scripts**
*   **Goal:** To create new scripts that will listen to the backend events and update the UI elements you just created.
*   **New Files to be Created:** `CharacterProfileUI.cs`, `ExperienceBarUI.cs`.
*   **Action:** When you have finished building the UI elements in Part 3, tell the Gemini assistant: **"I am at Step 4.1, please create the UI scripts."**

### Step 4.2: Link UI Scripts to UI Elements
*This step can only be done after the code change in 4.1 is complete.*
1.  Select the `CharacterProfilePanel` in the Hierarchy.
2.  Click **Add Component** and add the `CharacterProfileUI` script.
3.  Drag the `Strength_Text` object from the Hierarchy into the **Strength Text** field in the `CharacterProfileUI` component.
4.  Repeat for all other stat text fields.
5.  Select the `Experience_Slider` in the Hierarchy.
6.  Click **Add Component** and add the `ExperienceBarUI` script.
7.  Drag the `Experience_Slider` itself into the **Xp Slider** field.
8.  Drag the `Level_Text` object into the **Level Text** field.

---

## Part 5: Setting Up the Interaction UI (No Code Changes)

**Goal:** To display on-screen prompts when the player can interact with an object.

### Step 5.1: Create the Interaction Prompt UI
1.  In the **Hierarchy** window, right-click on your **Canvas** and select **UI > Text**. 
2.  Rename the new UI object to `InteractionPromptText`.
3.  Select the `InteractionPromptText` object.
4.  In the **Inspector**, find the **Rect Transform**. Click the anchor presets box, hold down `Alt`+`Shift`, and select the **bottom-center** anchor.
5.  Adjust the `Pos Y` field to around `50` to give it some space from the bottom of the screen.
6.  In the **Text** component, set the **Alignment** to center-middle.
7.  Choose a clear **Font Size** (e.g., 24) and **Color** (e.g., white).

### Step 5.2: Link the Prompt to the Player
1.  Select your player object in the Hierarchy.
2.  In the Inspector, find the **Player Interaction** script component.
3.  You will see a field named **Interaction Prompt Text**. Drag the `InteractionPromptText` object from your Hierarchy into this slot.

### Step 5.3: Configure Interaction Layers
1.  In the Unity Editor, go to **Edit > Project Settings**.
2.  Select the **Tags and Layers** panel.
3.  Under **Layers**, find an empty user layer (e.g., User Layer 8) and name it `Interactable`.
4.  Find any object in your scene that uses an `Interactable` script (like a `ResourceNode` or an `ItemPickup` prefab).
5.  Select the object. In the Inspector at the top right, click the **Layer** dropdown and select your new `Interactable` layer.
6.  A dialog may ask if you want to apply this to children. Choose **"Yes, change children."**
7.  Finally, select your player object. In the **Player Interaction** component, click the **Interaction Layer** dropdown and select the `Interactable` layer.