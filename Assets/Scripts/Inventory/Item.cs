using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public int Weight;
        public GameObject ItemPrefab;
    }
}

