using System;
using System.Linq;
using DefaultNamespace;
using DG.Tweening;
using Rullet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Inventories
{
    public abstract class BaseInventory : MonoBehaviour
    {
        [field: SerializeField] public InventoryType Type { get; protected set; }
        [field: SerializeField] public Slot ParentSlot { get; private set; }
        [field: SerializeField] public TMP_Text amountText { get; private set; }
        public int Amount { get; protected set; }
        protected InventoryManager InventoryManager { get; private set; }
        protected Image SpriteSlot { get; private set; }
        protected ImageType InventoryImage { get; set; }
        
        
        public virtual void Awake()
        {
            InventoryManager = FindObjectOfType<InventoryManager>();
            ParentSlot = GetComponentInParent<Slot>();
            SpriteSlot = GetComponent<Image>();
            
            DataBase.ResetData();
            
            Type = SelectRandomReward();
            
            InventoryManager.inventories.Add(this);
            PlaceBomb();
        }
        public virtual void OnDestroy()
        {
        }

        protected abstract void SelectInventory();

        public InventoryType SelectRandomReward()
        {
            Array values = Enum.GetValues(typeof(InventoryType));
            System.Random random = new System.Random();
            InventoryType randomInventory = (InventoryType)values.GetValue(random.Next(values.Length - 1));
            ParentSlot.slotType = SlotType.Reward;
            
            return randomInventory;
        }

        void PlaceBomb()
        {
            if (InventoryManager.inventories.Any(x => x.Type == InventoryType.Bomb))
                return;

            var randomIndex = Random.Range(0, InventoryManager.inventories.Count - 1);
            if (InventoryManager.inventories[randomIndex] == this)
                Type = InventoryType.Bomb;

            ParentSlot.slotType = SlotType.Bomb;   
        }


        public virtual void ResetInventories(int turnCount)
        {
            switch (turnCount)
            {
                case 5 :
                    Type = SelectRandomReward();
                    break;
                
                case 6 :
                    Type = SelectRandomReward();
                    PlaceBomb();
                    break;

                case 30 :
                    ParentSlot.slotType = SlotType.Reward;
                    Type = InventoryType.Gold;
                    break;
                
                case 31 : 
                    Type = SelectRandomReward();
                    PlaceBomb();
                    break;
            }

            transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .5f).SetEase(Ease.Linear)
                .SetLoops(2, LoopType.Yoyo);
        }

    }
}