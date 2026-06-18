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
- [ ] **Core RPG Systems (Further Development):**
    - [ ] **Skill System:** Design Skill Tree, create `SkillData` ScriptableObject, and script `SkillManager`.
    - [ ] **Gathering & Professions:** Script a `ProfessionManager` and create `ProfessionData` ScriptableObject.
- [ ] **Monster AI (Wild):** Script basic monster AI and spawner.
- [ ] **Monster Taming & Evolution System (Design Phase):** Create `PetData` and `FoodData` ScriptableObjects.
- [ ] **Blacksmithing Mini-game (Design Phase):** Design UI and script core logic.


## In Progress

- [X] **Project Documentation** (Ongoing)

## Completed

- [X] **Core RPG Backend Systems:**
    - [X] Designed and implemented a data-driven architecture using ScriptableObjects.
    - [X] **Enums & Modifiers:** Created `Enums.cs` for all stat and slot types.
    - [X] **Character Data:**
        - [X] `RaceData.cs`: Defines racial stats and evolution paths.
        - [X] `ClassData.cs`: Defines class progression, stats, and mastery bonuses.
    - [X] **Item & Equipment Data:**
        - [X] `ItemData.cs`: Base class for all items.
        - [X] `EquipmentData.cs`: Defines equippable items and their stat modifiers.
    - [X] **Core Manager Scripts:**
        - [X] `PlayerStats.cs`: Central hub for calculating all character stats from various sources.
        - [X] `InventoryManager.cs`: Manages adding, removing, and stacking items.
        - [X] `EquipmentManager.cs`: Manages equipping and unequipping items.
        - [X] `ClassManager.cs`: Manages class switching, leveling, and mastery.
- [X] **Project Initialization:**
    - [X] Set up Unity project with Starter Assets (URP).
    - [X] Established project vision and documentation (`PROJECT_VISION.md`, `TASK_TRACKER.md`, `CHANGELOG.md`).
- [X] **Core Scripting Foundation:**
    - [X] Create `AetherboundPlayerController.cs`.
    - [X] Create `AetherboundInputs.cs`.
    - [X] Create `AetherboundRigidBodyPush.cs`.
    - [X] Create `ManaManager.cs` singleton.
    - [X] Create `AetherboundInputActions.inputactions` asset with a "Magic" action.
