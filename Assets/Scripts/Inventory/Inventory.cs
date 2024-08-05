using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Inventory
{
    public class Inventory : MonoBehaviour
    {
        public event Action OnInventoryWeightChanged;
        [SerializeField] public List<InventorySlot> Items = new List<InventorySlot>();
        [SerializeField] public UnityEvent OnInventoryChanged;

        private float _currentInventoryWeight;
        private float _maxInventoryWeight = 20f;
        
        public float MaxInventoryWeight => _maxInventoryWeight;
        public float CurrentInventoryWeight
        {
            get => _currentInventoryWeight;
            set
            {
                _currentInventoryWeight = value;
                OnInventoryWeightChanged?.Invoke();
            }
        }

        public bool AddItems(Item item, int amount = 1)
        {
            foreach (var slot in Items)
            {
                if (slot.Item.ItemName == item.ItemName)
                {
                    slot.Amount += amount;
                    slot.Weight += item.Weight;
                    _currentInventoryWeight = slot.Weight;
                    OnInventoryChanged.Invoke();
                    return true;
                }
            }

            if (Items.Count > _maxInventoryWeight)
            {
                return false;
            }

            InventorySlot newSlot = new InventorySlot(item, amount);
            
            Items.Add(newSlot);

            OnInventoryChanged.Invoke();
            OnInventoryWeightChanged?.Invoke();

            return true;
        }

        public float CountCurrentInventoryWeight()
        {
            float currentWeight = 0;
            foreach (var item in Items)
            {
                currentWeight += item.Amount * item.Weight;
            }
            return currentWeight;
        }

        public Item GetItem(int i)
        {
            return i < Items.Count ? Items[i].Item : null;
        }

        public int GetAmount(int i)
        {
            return i < Items.Count ? Items[i].Amount : 0;
        }

        public int GetSize()
        {
            return Items.Count;
        }
        
        public void UpdateInventory(List<InventorySlot> newItems)
        {
            Items = newItems;
            _currentInventoryWeight = CountCurrentInventoryWeight();
            OnInventoryChanged.Invoke();
        }
        
        public void ClearOnInventoryWeightChanged()
        {
            if (OnInventoryWeightChanged != null)
            {
                foreach (var d in OnInventoryWeightChanged.GetInvocationList())
                {
                    OnInventoryWeightChanged -= (Action)d;
                }
            }
        }
    }
}

