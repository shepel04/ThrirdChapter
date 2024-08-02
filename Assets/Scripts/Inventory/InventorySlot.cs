using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item Item;
        public int Amount;
        public float Weight;

        public InventorySlot(Item item, int amount)
        {
            this.Item = item;
            this.Amount = amount;
            this.Weight = item.Weight;
        }
    }
}

