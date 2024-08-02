using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Inventory;
using Unity.VisualScripting;

namespace Inventory
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _amount;

        private Inventory _playerInventory;

        private void Start()
        {
            _playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        }

        public void OnRayReceived()
        {
            if (_playerInventory)
            {
                if (_playerInventory.CurrentInventoryWeight + _item.Weight <= _playerInventory.MaxInventoryWeight)
                {
                    _playerInventory.AddItems(_item, _amount);
                    Destroy(gameObject);
                }
                
            }
        }
    } 
}





