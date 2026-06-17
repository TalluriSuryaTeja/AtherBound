# Project Aetherbound TODO List

## Next Steps:

* [ ] **Update PlayerArmature Prefab:** 
  * Replace the default `ThirdPersonController`, `StarterAssetsInputs` and `PlayerInput` components with our new `AetherboundPlayerController`, `AetherboundInputs` and a new `PlayerInput` component that uses the `AetherboundInputActions` asset.

* [ ] **Animator Controller Setup:**
  * Add a "Magic" trigger parameter to the player's animator controller.
  * Create a new animation state for the magic attack and connect it to the other states using the "Magic" trigger.

* [ ] **Integrate Mana System:**
  * In `AetherboundPlayerController`, get a reference to the `ManaManager` component.
  * In the `HandleMagic` method, before triggering the animation, check if the player has enough mana using the `UseMana` method from the `ManaManager`.

* [ ] **Magic Spell Visuals:**
  * Create a particle system or other visual effect for the magic spell.
  * Instantiate this effect when the magic attack is triggered.

* [ ] **Magic Spell Logic:**
  * Implement the logic for what the magic spell actually does (e.g., deals damage, applies a status effect, etc.).