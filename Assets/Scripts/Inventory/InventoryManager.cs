using UnityEngine;

namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public Inventory inventory;

        public void AddItem(InventoryItem item)
        {
            if (!inventory.items.Contains(item))
            {
                inventory.items.Add(item);
                Debug.Log(item.itemName + " added to inventory.");
            }
        }

        public void RemoveItem(InventoryItem item)
        {
            if (inventory.items.Contains(item))
            {
                inventory.items.Remove(item);
                Debug.Log(item.itemName + " removed from inventory.");
            }
        }

        public void ListItems()
        {
            foreach (var item in inventory.items)
            {
                Debug.Log("Inventory contains: " + item.itemName);
            }
        }
    }
}

