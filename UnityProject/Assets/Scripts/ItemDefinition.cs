using UnityEngine;

namespace Gamedoido
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Gear,
        Consumable,
        Material
    }

    public enum ItemTier
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    [CreateAssetMenu(menuName = "Gamedoido/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public string itemName;
        public ItemType itemType;
        public ItemTier tier;
        [TextArea]
        public string description;

        public int basePower = 1;
        public int defense = 0;
        public int durability = 100;
    }
}
