using System.Collections.Generic;
using UnityEngine;

namespace Gamedoido
{
    public class UpgradeSystem : MonoBehaviour
    {
        [System.Serializable]
        public class UpgradeLevel
        {
            public int level;
            public int powerBonus;
            public int defenseBonus;
            public int durabilityBonus;
        }

        public List<UpgradeLevel> upgradeLevels = new List<UpgradeLevel>();

        private readonly Dictionary<ItemDefinition, int> _itemLevels = new Dictionary<ItemDefinition, int>();

        public int GetLevel(ItemDefinition item)
        {
            if (item == null)
            {
                return 0;
            }

            return _itemLevels.TryGetValue(item, out int level) ? level : 0;
        }

        public bool UpgradeItem(ItemDefinition item)
        {
            if (item == null)
            {
                return false;
            }

            int currentLevel = GetLevel(item);
            int nextLevel = currentLevel + 1;

            UpgradeLevel targetLevel = upgradeLevels.Find(level => level.level == nextLevel);
            if (targetLevel == null)
            {
                return false;
            }

            _itemLevels[item] = nextLevel;
            return true;
        }

        public void ApplyUpgradeStats(ItemDefinition item, ref int power, ref int defense, ref int durability)
        {
            if (item == null)
            {
                return;
            }

            int level = GetLevel(item);
            UpgradeLevel current = upgradeLevels.Find(upgrade => upgrade.level == level);
            if (current == null)
            {
                return;
            }

            power += current.powerBonus;
            defense += current.defenseBonus;
            durability += current.durabilityBonus;
        }
    }
}
