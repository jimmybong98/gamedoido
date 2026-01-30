# Gamedoido - Medieval Survival RPG (Base Systems)

This repository contains a base Unity project layout and core gameplay systems for a 3D open-world medieval survival RPG. The scripts focus on survival mechanics, loot/upgrade systems, enemy archetypes (human, zombie, bosses), and lootable crates spread across the map.

## Goals covered
- 3D open-world medieval setting
- Survival mechanics (health, stamina, hunger, thirst)
- Loot system with crates and tiered loot tables
- Upgrade system for armor, gear, and weapons
- Enemies: human, zombie, and bosses

## Getting started (Unity)
1. Create a new Unity 3D project (URP recommended).
2. Copy the `UnityProject/Assets` folder into your Unity project `Assets` directory.
3. Create ScriptableObject assets for items and loot tables from the **Assets > Create > Gamedoido** menu.
4. Add the provided components to scene GameObjects:
   - **PlayerSurvival** on the player controller
   - **Inventory** on the player
   - **UpgradeSystem** on the player
   - **LootCrate** on crate prefabs
   - **WorldSpawner** on a manager object

## Notes
This is a base gameplay layer intended to be extended with:
- terrain generation/streaming
- navmesh and AI behavior trees
- animation and VFX
- networking/multiplayer

See `Docs/GameDesign.md` for a full design overview and how systems connect.
