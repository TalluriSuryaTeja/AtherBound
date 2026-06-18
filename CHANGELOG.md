# Project Aetherbound Changelog

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
