# Medieval Survival RPG - Base Design

## Core loop
1. Explore the open world and collect resources.
2. Loot crates to obtain armor, gear, and weapons.
3. Fight humans, zombies, and bosses to secure upgrades.
4. Manage survival stats (health, stamina, hunger, thirst).
5. Upgrade equipment to survive tougher regions.

## World structure
- Open-world medieval biome with villages, ruins, and dungeon entrances.
- Crate density increases in high-risk zones.
- Boss arenas are fixed points of interest.

## Systems overview
### Survival system
- Health, stamina, hunger, thirst, temperature.
- Hunger/thirst reduce max stamina and regen.

### Loot system
- Crates have weighted loot tables.
- Items are tiered (Common, Uncommon, Rare, Epic, Legendary).

### Upgrade system
- Upgrade stations in towns or portable kits.
- Use materials and gold to upgrade gear/armor.

### Enemy archetypes
- Humans: weapon users, higher intelligence.
- Zombies: slower, high health, infection effects.
- Bosses: unique abilities and guaranteed loot.

## Technical approach
- ScriptableObjects for items and loot tables.
- Modular MonoBehaviour components for systems.
- A centralized world spawner to populate enemies and loot crates.
