using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemSelector : MonoBehaviour
    {
        public InventorySlot SelectedInventorySlot;
        
        [SerializeField] private List<Image> _images;
        [SerializeField] private Inventory _playerInventory;

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
                if (SelectedInventorySlot != null)
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
            if (selectedInventorySlot.Amount < 1)
            {
                selectedInventorySlot = null;
                _images[_selectedItemIndex] = null;
                _playerInventory.OnInventoryChanged.Invoke();
            }
            else
            {
                selectedInventorySlot.Amount -= 1;
                _playerInventory.OnInventoryChanged.Invoke();
            }
        }
    }
}


