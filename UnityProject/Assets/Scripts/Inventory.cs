using System.Collections.Generic;
using UnityEngine;

namespace Gamedoido
{
    public class Inventory : MonoBehaviour
    {
        public List<ItemDefinition> items = new List<ItemDefinition>();

        public void AddItem(ItemDefinition item)
        {
            if (item == null)
            {
                return;
            }

            items.Add(item);
        }

        public bool RemoveItem(ItemDefinition item)
        {
            return items.Remove(item);
        }
    }
}
