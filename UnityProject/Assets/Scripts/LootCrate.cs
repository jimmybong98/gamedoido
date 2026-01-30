using UnityEngine;

namespace Gamedoido
{
    public class LootCrate : MonoBehaviour
    {
        public LootTable lootTable;
        public int rolls = 1;
        public bool isOpened;

        public void OpenCrate(Inventory inventory)
        {
            if (isOpened || lootTable == null || inventory == null)
            {
                return;
            }

            isOpened = true;

            for (int i = 0; i < rolls; i++)
            {
                ItemDefinition loot = lootTable.RollLoot();
                if (loot != null)
                {
                    inventory.AddItem(loot);
                }
            }
        }
    }
}
