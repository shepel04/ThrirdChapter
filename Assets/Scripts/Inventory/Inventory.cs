using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Inventory/Inventory")]
    public class Inventory : ScriptableObject
    {
        public List<InventoryItem> items = new List<InventoryItem>();
    }
}

