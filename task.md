# AetherBound - Project Plan & Status

## Game Overview
- **Genre**: 3D Co-op Survival and Automation.
- **Setting**: Sky Islands with a steampunk aesthetic.
- **Core Loop**: Gather resources (Wood, Coal, Metal) -> Build power sources/defenses -> Defend against monsters at night -> Build an Air Balloon to reach new Sky Islands.
- **Progression**: Update world from stone age to technology era. Includes magic (Aether/mana).
- **Technical Specs**: Unity (URP), Laptop targeted (efficient code), 2-Player Local Co-op (one keyboard).
- **Controls**:
  - Player 1: WASD + E to interact.
  - Player 2: Arrows + M to interact / Gamepad supported.

## Task List

### Phase 1: Foundation (Current Status)
- [x] Create 'Scripts' folder.
- [x] Create `GameManager.cs` to track Wood, Coal, Metal, and Aether.
- [x] Create `PlayerController.cs` supporting 2-player control scheme.
- [x] Create `ResourceNode.cs` for gathering mechanics.
- [x] Create `UIManager.cs` to display resources.
- [x] Unity Editor: Import and setup Resource Prefabs (fix URP materials).
- [x] Unity Editor: Setup Player Prefab (Collider, Rigidbody, PlayerController).

### Phase 2: Core Systems (Next Up)
- [ ] Building System (Spend resources to place power sources, defenses, Air Balloon).
- [ ] Day/Night Cycle Manager.
- [ ] Monster Spawning & Enemy AI for night defense.
- [ ] Player Health & Damage System.

### Phase 3: Progression & Polish
- [ ] Era progression (Stone Age -> Tech Era).
- [ ] Island traversal (Air Balloon mechanics).
- [ ] Steampunk/Magic visual and audio polish.
