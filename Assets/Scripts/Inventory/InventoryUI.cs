using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private List<Image> _icons = new List<Image>();
        [SerializeField] private List<TMP_Text> _amounts = new List<TMP_Text>();

        public void UpdateUI(Inventory inventory)
        {
            for (int i = 0; i < inventory.GetSize() && i < _icons.Count; i++)
            {
                
                _icons[i].color = new Color(1, 1, 1, 1);
                
                
                _icons[i].sprite = inventory.GetItem(i).Icon;
                _amounts[i].text = inventory.GetAmount(i) >= 1 ? inventory.GetAmount(i).ToString() : "";
            }

            for (int i = inventory.GetSize(); i < _icons.Count; i++)
            {
                _icons[i].color = new Color(1, 1, 1, 0);
                
                _icons[i].sprite = null;
                _amounts[i].text = "";
            }
        }
    } 
}


