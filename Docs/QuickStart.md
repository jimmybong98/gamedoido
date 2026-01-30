# Quick-start playable scene (placeholder models)

This repo only ships with scripts, so the quick-start workflow builds a playable test scene at runtime using Unity primitives as **placeholder 3D models**. These primitives let you test movement, survival drain, inventory, and crate interaction without hand-authored art.

## How it works
- **QuickStartSceneBuilder** auto-spawns a ground plane, lighting, a player controller, and loot crates when you press Play (only if no player exists in the scene).
- The scene uses **primitive meshes** (capsule, cube, plane) as stand-in models.
- Loot is generated from in-memory `ScriptableObject` instances, so you can test crates without creating assets first.

## Controls (Play Mode)
- **WASD** move
- **Shift** sprint
- **Space** jump
- **Mouse** look
- **E** interact with crates
- **Esc** toggle cursor lock

## Customization
In Unity, create an empty GameObject and add **QuickStartSceneBuilder**. From there you can:
- toggle `spawnEnemies` (requires a baked NavMesh)
- increase `crateCount`
- adjust `worldSize` and `crateSpacing`

When you are ready, replace the primitive meshes with real 3D assets and disable/remove the quick-start builder.
