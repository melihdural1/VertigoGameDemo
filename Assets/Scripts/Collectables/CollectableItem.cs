using Controllers;
using DefaultNamespace;
using Inventories;
using Rullet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Collectables
{
    public class CollectableItem : MonoBehaviour
    {
        public CollectableManager collectableManager;
        public InventoryType type;
        public TMP_Text amountText;
        private int _amount;

        private void Awake()
        {
            collectableManager.collectableItems.Add(this);
            EventController.OnDataUpdate += UpdateText;
            EventController.OnPlayerFail += ResetText;
        }

        private void Start()
        {
            amountText.text = DataBase.GetData(type).ToString();
        }

        private void OnDestroy()
        {
            EventController.OnDataUpdate -= UpdateText;
            EventController.OnPlayerFail -= ResetText;
        }


        void UpdateText(Slot slot)
        {
            if (slot.inventory.Type == type)
            {
                _amount += slot.inventory.Amount;
                amountText.text = _amount.ToString();
            }
        }

        private void ResetText()
        {
            amountText.text = 0.ToString();
            _amount = 0;
        }
    }
}