# AetherBound: Task Tracker

## To Do

- [ ] **Player Setup & Integration:**
    - [ ] Update `PlayerArmature` Prefab: Replace default scripts with `AetherboundPlayerController`, `AetherboundInputs`, and configure the `PlayerInput` component for `AetherboundInputActions`.
    - [ ] Animator Controller: Add a "Magic" trigger parameter and create the magic attack animation state.
    - [ ] Mana Integration: Connect `ManaManager` to `AetherboundPlayerController` to use mana for magic.
- [ ] **UI Implementation:**
    - [ ] Create UI for Mana bars, Day/Night clock, Inventory, Character Profile, Skill Tree, and Profession progress.
- [ ] **Core RPG Systems (Further Development):**
    - [ ] **Skill System:** Design Skill Tree, script `SkillManager`.
- [ ] **Monster Taming & Evolution System (Design Phase):** Create `PetData` and `FoodData` ScriptableObjects.
- [ ] **Blacksmithing Mini-game (Design Phase):** Design UI and script core logic.


## In Progress

- [X] **Project Documentation** (Ongoing)

## Completed

- [X] **Gathering & Professions:**
    - [X] Script a `ProfessionManager` and create `ProfessionData` ScriptableObject.
- [X] **Game Logic & World:**
    - [X] Created `GameManager.cs` to manage the Day/Night cycle. The system dynamically alters monster stats and loot based on the time of day.
    - [X] Created `ResourceNode.cs` to allow for the creation of gatherable resources in the world.
- [X] **Monster Spawning System:**
    - [X] Created `MonsterSpawner.cs` to handle spawning groups of monsters within a defined radius.
    - [X] Implemented automatic respawning after a configurable delay.
- [X] **Basic Monster AI:** 
    - [X] Created `MonsterAIController.cs` with a state machine for Patrolling, Chasing, and Attacking.
    - [X] Integrated with `NavMeshAgent` for movement.
    - [X] Added player detection and basic combat engagement logic.
- [X] **Monster & Skill Data Systems:**
    - [X] Created `MonsterData.cs` ScriptableObject for monster archetypes.
    - [X] Created `MonsterStats.cs` MonoBehaviour to manage live monster data.
    - [X] Created universal `SkillData.cs` for player and monster abilities.
- [X] **Input System with Remapping:**
    - [X] Created `InputManager.cs` singleton to handle player input and control rebinding.
    - [X] Implemented saving and loading of custom keybindings.
    - [X] Created `SettingsMenuInputUI.cs` to manage the UI for the controls menu.
    - [X] Authored `INPUT_SYSTEM_GUIDE.md` with full setup instructions.
- [X] **Core RPG Backend Systems:**
    - [X] Designed and implemented a data-driven architecture using ScriptableObjects.
    - [X] `Enums.cs`, `RaceData.cs`, `ClassData.cs`, `ItemData.cs`, `EquipmentData.cs`.
    - [X] `PlayerStats.cs`, `InventoryManager.cs`, `EquipmentManager.cs`, `ClassManager.cs`.
- [X] **Project Initialization & Core Scripts:**
    - [X] Set up Unity project and created initial player controller and input scripts.
