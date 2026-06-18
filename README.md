# Aetherbound

## Project Vision

**Genre:** 3D Co-op Survival, Automation, and Exploration RPG.

**Setting:** Floating Sky Islands with a "Stylized Realism" (Steampunk/Ghibli) aesthetic.

**Core Loop:**

*   **Day:** Gather, craft, build, and manage tamed creatures.
*   **Night:** Defend the base against waves of powerful, aggressive monsters.

**Progression:** Advance through technology tiers, level up characters and professions, and evolve powerful companion creatures.

## Key Mechanics

*   **Dynamic World Systems:**
    *   **Day/Night Cycle:** The world transitions between day and night, managed by a central `GameManager`. This cycle is not just cosmetic; at night, monsters become significantly stronger, more aggressive, and drop rarer loot, creating a high-risk, high-reward environment.
    *   **Resource Gathering:** The world is populated with `ResourceNode` objects (e.g., trees, ore veins, herbs) that players can gather from. These nodes have defined gathering times and will respawn after being depleted, forming the foundation of the crafting and profession systems.
*   **Player Progression Systems:**
    *   **Inventory System:** A grid-based inventory for managing resources, tools, and gear.
    *   **Character Profile & Stats:** Players have core stats (e.g., Strength, Dexterity, Intelligence) that affect gameplay.
    *   **Leveling System:** Gain XP from combat, gathering, and crafting to level up and improve stats.
    *   **Skill System:** Unlock active and passive skills from a skill tree as you level up.
*   **Gathering & Professions:**
    *   **Gathering Skills:** Players level up specific skills like Logging and Mining by performing the actions.
    *   **Profession Tiers:** Professions like Blacksmith and Alchemist level up with use. Higher levels unlock advanced recipes and the ability to work with rare materials.
    *   **Aether-Infused Tiers:** Top-tier professions can become Aether-infused (e.g., "Aethersmith"), allowing the crafting of magical equipment.
*   **Blacksmithing:** A hands-on system where players hammer and fold metal to craft superior gear.
*   **Magic System:** Players use "Aether" as a resource for spells.
*   **Monster Taming & Evolution:**
    *   Players can tame wild monsters, which evolve based on their diet.

## Task Tracker

### To Do

*   **Player Setup & Integration:**
    *   Update `PlayerArmature` Prefab: Replace default scripts with `AetherboundPlayerController`, `AetherboundInputs`, and configure the `PlayerInput` component for `AetherboundInputActions`.
    *   Animator Controller: Add a "Magic" trigger parameter and create the magic attack animation state.
    *   Mana Integration: Connect `ManaManager` to `AetherboundPlayerController` to use mana for magic.
*   **UI Implementation:**
    *   Create UI for Mana bars, Day/Night clock, Inventory, Character Profile, Skill Tree, and Profession progress.
*   **Core RPG Systems (Further Development):**
    *   **Skill System:** Design Skill Tree, script `SkillManager`.
    *   **Gathering & Professions:** Script a `ProfessionManager` and create `ProfessionData` ScriptableObject.
*   **Monster Taming & Evolution System (Design Phase):** Create `PetData` and `FoodData` ScriptableObjects.
*   **Blacksmithing Mini-game (Design Phase):** Design UI and script core logic.

### In Progress

*   **Project Documentation** (Ongoing)

### Completed

*   **Game Logic & World:**
    *   Created `GameManager.cs` to manage the Day/Night cycle. The system dynamically alters monster stats and loot based on the time of day.
    *   Created `ResourceNode.cs` to allow for the creation of gatherable resources in the world.
*   **Monster Spawning System:**
    *   Created `MonsterSpawner.cs` to handle spawning groups of monsters within a defined radius.
    *   Implemented automatic respawning after a configurable delay.
*   **Basic Monster AI:**
    *   Created `MonsterAIController.cs` with a state machine for Patrolling, Chasing, and Attacking.
    *   Integrated with `NavMeshAgent` for movement.
    *   Added player detection and basic combat engagement logic.
*   **Monster & Skill Data Systems:**
    *   Created `MonsterData.cs` ScriptableObject for monster archetypes.
    *   Created `MonsterStats.cs` MonoBehaviour to manage live monster data.
    *   Created universal `SkillData.cs` for player and monster abilities.
*   **Input System with Remapping:**
    *   Created `InputManager.cs` singleton to handle player input and control rebinding.
    *   Implemented saving and loading of custom keybindings.
    *   Created `SettingsMenuInputUI.cs` to manage the UI for the controls menu.
    *   Authored `INPUT_SYSTEM_GUIDE.md` with full setup instructions.
*   **Core RPG Backend Systems:**
    *   Designed and implemented a data-driven architecture using ScriptableObjects.
    *   `Enums.cs`, `RaceData.cs`, `ClassData.cs`, `ItemData.cs`, `EquipmentData.cs`.
    *   `PlayerStats.cs`, `InventoryManager.cs`, `EquipmentManager.cs`, `ClassManager.cs`.
*   **Project Initialization & Core Scripts:**
    *   Set up Unity project and created initial player controller and input scripts.

## Changelog

### [2024-05-23]

*   **Day/Night Cycle & Effects:**
    *   Created `GameManager.cs` as a singleton to manage the global Day/Night cycle.
    *   The cycle dynamically affects monster behavior, increasing their aggression and power at night.
    *   Monsters now have a chance to drop rare items when defeated at night.
    *   The system is event-driven, allowing other game systems to easily react to time changes.
*   **Resource Gathering System:**
    *   Created `ResourceNode.cs`, a component that turns any game object into a gatherable resource (e.g., ore, herbs).
    *   Nodes define the item they yield, the time it takes to gather, and a respawn timer.
    *   The system manages its own state, automatically handling depletion and respawning.

*   Updated `MonsterStats.cs` to subscribe to the Day/Night cycle events and apply stat boosts from `MonsterData`.
*   Updated `MonsterAIController.cs` to increase detection radius and movement speed at night.
*   Updated `MonsterData.cs` to include fields for night-time stat multipliers and a separate rare loot table.
