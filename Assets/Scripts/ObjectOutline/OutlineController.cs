using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

namespace ObjectOutline
{
    public class OutlineController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private LayerMask _layerMask;
    
        private Outline _currentOutline;
        private Inventory.Inventory _playerInventory; 

        private void Start()
        {
            _playerInventory = GetComponent<Inventory.Inventory>();
        }

        void Update()
        {
            HandleHighlighting();
        }

        private void HandleHighlighting()
        {
            Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.green);

            if (Physics.Raycast(ray, out hit, _maxDistance, _layerMask))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    if (Input.GetButton("Fire1"))
                    {
                        CollectItem(hit);
                    }
                    
                    if (_currentOutline != outline)
                    {
                        ClearOutline();
                        _currentOutline = outline;
                        _currentOutline.enabled = true;
                    }
                }
                else
                {
                    ClearOutline();
                }
            }
            else
            {
                ClearOutline();

            }
        }

        private void CollectItem(RaycastHit hit)
        {   
            hit.collider.GetComponent<CollectableItem>().OnRayReceived();
        }

        private void ClearOutline()
        {
            if (_currentOutline != null)
            {
                _currentOutline.enabled = false;
                _currentOutline = null;
            }
        }
    }
}


