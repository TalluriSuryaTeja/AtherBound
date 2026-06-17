# AetherBound: Task Tracker

## To Do

- [ ] **Player Setup & Integration:**
    - [ ] Update `PlayerArmature` Prefab: Replace default scripts with `AetherboundPlayerController`, `AetherboundInputs`, and configure the `PlayerInput` component for `AetherboundInputActions`.
    - [ ] Animator Controller: Add a "Magic" trigger parameter and create the magic attack animation state.
    - [ ] Mana Integration: Connect `ManaManager` to `AetherboundPlayerController` to use mana for magic.
- [ ] **Game Logic & World:**
    - [ ] Update `GameManager.cs` with Day/Night cycle and game state.
    - [ ] Update `ResourceNode.cs` script.
- [ ] **UI Implementation:**
    - [ ] Create UI for Mana bars, Day/Night clock, Inventory, Character Profile, Skill Tree, and Profession progress.
- [ ] **Core RPG Systems (Design Phase):**
    - [ ] **Inventory System:** Script `InventoryManager` and create `ItemData` ScriptableObject.
    - [ ] **Character Stats & Leveling:** Script `PlayerStats` component and design XP curve.
    - [ ] **Skill System:** Design Skill Tree, create `SkillData` ScriptableObject, and script `SkillManager`.
    - [ ] **Gathering & Professions:** Script a `ProfessionManager` and create `ProfessionData` ScriptableObject.
- [ ] **Monster AI (Wild):** Script basic monster AI and spawner.
- [ ] **Monster Taming & Evolution System (Design Phase):** Create `PetData` and `FoodData` ScriptableObjects.
- [ ] **Blacksmithing Mini-game (Design Phase):** Design UI and script core logic.


## In Progress

- [X] **Project Documentation** (Ongoing)

## Completed

- [X] **Project Initialization:**
    - [X] Set up Unity project with Starter Assets (URP).
    - [X] Established project vision and documentation (`PROJECT_VISION.md`, `TASK_TRACKER.md`, `CHANGELOG.md`).
- [X] **Core Scripting Foundation:**
    - [X] Create `AetherboundPlayerController.cs`.
    - [X] Create `AetherboundInputs.cs`.
    - [X] Create `AetherboundRigidBodyPush.cs`.
    - [X] Create `ManaManager.cs` singleton.
    - [X] Create `AetherboundInputActions.inputactions` asset with a "Magic" action.