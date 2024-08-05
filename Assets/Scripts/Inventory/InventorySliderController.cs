using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySliderController : MonoBehaviour
    {
        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Slider _inventorySlider;
        [SerializeField] private ItemSelector _itemSelector;

        private void Start()
        {
            _inventorySlider.maxValue = _playerInventory.MaxInventoryWeight;
            _playerInventory.OnInventoryWeightChanged += SetInventoryWeight;
            _itemSelector.OnInventoryWeightChanged += SetInventoryWeight;
            SetInventoryWeight();
        }
        
        private void OnDestroy()
        {
            _playerInventory.OnInventoryWeightChanged -= SetInventoryWeight;
        }

        private void Update()
        {
            SetInventoryWeight();
        }
        
        public void SetInventoryWeight()
        {
            _inventorySlider.value = _playerInventory.CurrentInventoryWeight;
        }
    }
}


