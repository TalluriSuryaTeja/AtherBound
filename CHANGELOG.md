# Project Aetherbound Changelog

## [2024-05-24]

### Fixed
- **Compilation Errors:**
    - Refactored the stat system to use a single, unified `StatType` enum, resolving widespread compilation errors caused by conflicting `Stat` and `StatType` enums.
    - Updated `MonsterStats.cs` to correctly use the `StatType` enum and its associated data structures.

### Added
- **Profession System Events:**
    - Implemented `onExperienceChanged` and `onLevelUp` events in `ProfessionManager.cs` to allow UI elements to react to profession progression.
    - Added `GetExperience`, `GetXPForNextLevel`, and `GetLevel` methods to provide the necessary data for the UI.

## [2024-05-23]

### Added
- **Day/Night Cycle & Effects:**
    - Created `GameManager.cs` as a singleton to manage the global Day/Night cycle.
    - The cycle dynamically affects monster behavior, increasing their aggression and power at night.
    - Monsters now have a chance to drop rare items when defeated at night.
    - The system is event-driven, allowing other game systems to easily react to time changes.

- **Resource Gathering System:**
    - Created `ResourceNode.cs`, a component that turns any game object into a gatherable resource (e.g., ore, herbs).
    - Nodes define the item they yield, the time it takes to gather, and a respawn timer.
    - The system manages its own state, automatically handling depletion and respawning.

### Changed
- Updated `MonsterStats.cs` to subscribe to the Day/Night cycle events and apply stat boosts from `MonsterData`.
- Updated `MonsterAIController.cs` to increase detection radius and movement speed at night.
- Updated `MonsterData.cs` to include fields for night-time stat multipliers and a separate rare loot table.

## [2024-05-22]

### Added
- **Monster Spawning System:**
    - Created `MonsterSpawner.cs`, a component that can be placed in the world to spawn groups of monsters.
    - Spawners define the type and number of monsters, a spawn radius, and a respawn time.
    - The system automatically handles respawning monsters after they have been defeated.

- **Basic Monster AI:**
    - Created `MonsterAIController.cs`, a `MonoBehaviour` that gives monsters basic intelligence.
    - Implemented a state machine with `Patrolling`, `Chasing`, and `Attacking` states.
    - The AI uses a `NavMeshAgent` for pathfinding and can detect the player within a configurable radius.
    - Added editor gizmos to visualize the monster's detection and attack ranges.

- **Monster & Skill Data Systems:**
    - Created `MonsterData.cs`, a `ScriptableObject` to define monster archetypes, including their base stats, leveling progression, skills, and loot tables.
    - Created `MonsterStats.cs`, a `MonoBehaviour` to manage the in-game state and stats of individual monsters.
    - Created a universal `SkillData.cs` `ScriptableObject` that can be used by both players and monsters to define skills and their effects.

- **Remappable Input System:**
    - Created a new `InputManager.cs` singleton to manage all player input, including support for both Keyboard/Mouse and Gamepad.
    - Implemented functionality to save, load, and reset control bindings locally using `PlayerPrefs`.
    - Added a `SettingsMenuInputUI.cs` script to easily connect UI buttons to the input rebinding logic.
    - Wrote `INPUT_SYSTEM_GUIDE.md`, a comprehensive guide for setting up the Input Actions asset and the controls menu in the Unity Editor.

## [2024-05-21]

### Added
- **Core RPG Backend Systems:** A comprehensive, data-driven backend for character progression and items.
- **ScriptableObject Architecture:** Implemented a robust architecture using ScriptableObjects for flexible data management.
    - `RaceData.cs` for defining character races and their evolutionary paths.
    - `ClassData.cs` for defining character classes, their progression, and mastery bonuses.
    - `ItemData.cs` and `EquipmentData.cs` for creating a wide variety of items and equipment.
- **Manager Scripts:** Added a suite of powerful manager scripts to handle game logic.
    - `PlayerStats.cs`: A central manager to calculate and consolidate all player stats.
    - `ClassManager.cs`: Manages class switching, XP, leveling, and mastery.
    - `EquipmentManager.cs`: Handles the equipping and unequipping of items.
    - `InventoryManager.cs`: A complete inventory system with support for item stacking.
- **Project Documentation:** Created a `UNITY_SETUP_GUIDE.md` to provide a manual, step-by-step guide for setting up the backend and UI in the Unity Editor.

## [Initial Setup - YYYY-MM-DD]

### Added
* Created `AetherboundInputs.cs` to handle player input, including a new "Magic" input.
* Created `AetherboundPlayerController.cs`, a custom player controller adapted from the Starter Assets' `ThirdPersonController`.
* Created `AetherboundInputActions.inputactions` to define the new "Magic" input binding.
* Created `AetherboundRigidBodyPush.cs` to handle pushing rigidbodies.
* Created `ManaManager.cs` to manage the player's mana.
* Created `TASK_TRACKER.md` to track project tasks.
* Created `CHANGELOG.md` to track project changes.
