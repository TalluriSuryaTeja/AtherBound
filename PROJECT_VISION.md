# AetherBound: Project Vision

## 1. Core Concept
- **Genre:** 3D Co-op Survival, Automation, and Exploration RPG.
- **Setting:** Floating Sky Islands with a "Stylized Realism" (Steampunk/Ghibli) aesthetic.
- **Core Loop:**
    - **Day:** Gather, craft, build, and manage tamed creatures.
    - **Night:** Defend the base against waves of wild monsters.
- **Progression:** Advance through technology tiers, level up characters and professions, and evolve powerful companion creatures.

## 2. Key Mechanics
- **Player Progression Systems:**
    - **Inventory System:** A grid-based inventory for managing resources, tools, and gear.
    - **Character Profile & Stats:** Players have core stats (e.g., Strength, Dexterity, Intelligence) that affect gameplay.
    - **Leveling System:** Gain XP from combat, gathering, and crafting to level up and improve stats.
    - **Skill System:** Unlock active and passive skills from a skill tree as you level up.
- **Gathering & Professions:**
    - **Gathering Skills:** Players level up specific skills like Logging and Mining by performing the actions.
    - **Profession Tiers:** Professions like Blacksmith and Alchemist level up with use. Higher levels unlock advanced recipes and the ability to work with rare materials. 
    - **Aether-Infused Tiers:** Top-tier professions can become Aether-infused (e.g., "Aethersmith"), allowing the crafting of magical equipment.
- **Blacksmithing:** A hands-on system where players hammer and fold metal to craft superior gear.
- **Magic System:** Players use "Aether" as a resource for spells.
- **Monster Taming & Evolution:**
    - Players can tame wild monsters, which evolve based on their diet.

## 3. Technical Specifications
- **Engine:** Unity 6 (URP).
- **Performance Goal:** 60 FPS in 2-player local co-op.
- **Input System:** Unity's New Input System.

## 4. Local Co-op Input Scheme
- **Player 1 (Keyboard & Mouse):** Standard FPS controls.
- **Player 2 (Gamepad):** Standard 3rd-person controller.

## 5. Development Guidelines
- **Language:** C#.
- **Modularity:** One Script per Task.
- **Data:** Use ScriptableObjects for all data assets.
- **Architecture:** Singletons for global managers.
