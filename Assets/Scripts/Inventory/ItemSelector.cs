using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemSelector : MonoBehaviour
    {
        public event Action OnInventoryWeightChanged;
        public InventorySlot SelectedInventorySlot;
        
        [SerializeField] private List<Image> _images;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Slider _inventorySlider;

        private int _selectedItemIndex;

        private void Update()
        {
            for (int i = 0; i < _images.Count; i++)
            {
                if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
                {
                    MoveSelectionFrame(i);
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                if (_playerInventory.Items.Count > 0)
                {
                    OnInventoryWeightChanged?.Invoke();
                    RemoveSelectedItem(SelectedInventorySlot);
                }
            }
        }

        private void MoveSelectionFrame(int index)
        {
            transform.position = _images[index].transform.position;
            if (index <= _playerInventory.Items.Count - 1)
            {
                SelectedInventorySlot = _playerInventory.Items[index];
                _selectedItemIndex = index;
            }
        }

        private void RemoveSelectedItem(InventorySlot selectedInventorySlot)
        {
            if (selectedInventorySlot.Amount == 1)
            {
                _playerInventory.CurrentInventoryWeight = 0;
                _playerInventory.Items.Remove(selectedInventorySlot);
                SpawnItemInWorld(selectedInventorySlot.Item, selectedInventorySlot);

                _inventoryUI.UpdateUI(_playerInventory);

                _playerInventory.OnInventoryChanged.Invoke();
            }
            else if (selectedInventorySlot.Amount > 1)
            {
                _playerInventory.CurrentInventoryWeight -= selectedInventorySlot.Item.Weight;
                selectedInventorySlot.Amount -= 1;
                selectedInventorySlot.Weight -= selectedInventorySlot.Item.Weight;   
                SpawnItemInWorld(selectedInventorySlot.Item, selectedInventorySlot);
                _playerInventory.OnInventoryChanged.Invoke();
            }
        }

        private void SpawnItemInWorld(Item inventoryItem, InventorySlot inventorySlot)
        {
            if (inventoryItem != null && inventoryItem.ItemPrefab != null && inventorySlot.Amount > 0)
            {
                Vector3 spawnPosition = _playerTransform.position + _playerTransform.forward;
                GameObject spawnedItem = Instantiate(inventoryItem.ItemPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
