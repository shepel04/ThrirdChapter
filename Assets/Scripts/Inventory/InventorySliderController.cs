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

        private void Start()
        {
            _inventorySlider.maxValue = _playerInventory.MaxInventoryWeight;
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


