using System.Collections.Generic;
using UnityEngine;

namespace Gamedoido
{
    [CreateAssetMenu(menuName = "Gamedoido/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [System.Serializable]
        public class LootEntry
        {
            public ItemDefinition item;
            [Range(1, 100)]
            public int weight = 10;
        }

        public List<LootEntry> entries = new List<LootEntry>();

        public ItemDefinition RollLoot()
        {
            if (entries.Count == 0)
            {
                return null;
            }

            int totalWeight = 0;
            foreach (var entry in entries)
            {
                totalWeight += Mathf.Max(0, entry.weight);
            }

            int roll = Random.Range(0, totalWeight);
            int cumulative = 0;

            foreach (var entry in entries)
            {
                cumulative += Mathf.Max(0, entry.weight);
                if (roll < cumulative)
                {
                    return entry.item;
                }
            }

            return entries[entries.Count - 1].item;
        }
    }
}
