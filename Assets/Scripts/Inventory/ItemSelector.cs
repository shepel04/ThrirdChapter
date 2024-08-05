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
        public InventorySlot SelectedInventorySlot;
        
        [SerializeField] private List<Image> _images;
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private Transform _playerTransform;

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
                if (SelectedInventorySlot != null && SelectedInventorySlot.Amount >= 1)
                {
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
                selectedInventorySlot.Amount -= 1;
                selectedInventorySlot.Weight -= selectedInventorySlot.Item.Weight;
                
                selectedInventorySlot.Weight = 0;
                selectedInventorySlot.Amount = 0;
                selectedInventorySlot.Item = null;
                _playerInventory.Items.Remove(selectedInventorySlot);

                // _inventoryUI.UpdateUI(_playerInventory);

                // _playerInventory.OnInventoryChanged.Invoke();
            }
            else if (selectedInventorySlot.Amount != 0)
            {
                selectedInventorySlot.Amount -= 1;
                selectedInventorySlot.Weight -= selectedInventorySlot.Item.Weight;   
                _playerInventory.OnInventoryChanged.Invoke();
            }
            

            if (selectedInventorySlot != null && selectedInventorySlot.Amount > 0)
            {
                SpawnItemInWorld(selectedInventorySlot.Item, selectedInventorySlot);
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
