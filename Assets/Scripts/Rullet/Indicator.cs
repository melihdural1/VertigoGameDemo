using System.Collections.Generic;
using Controllers;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Rullet
{
    public class Indicator : MonoBehaviour
    {
        public Image indicatorImage;
        public List<Sprite> indicatorImages;

        public float rayRotation;

    
    
        private void Awake()
        {
            EventController.OnSpinEnd += DetectSlot;
            EventController.OnSpinStateChange += ChangeState;
        }
    


        private void OnDestroy()
        {
            EventController.OnSpinEnd -= DetectSlot;
            EventController.OnSpinStateChange -= ChangeState;
        }

        void DetectSlot(int spinCount)
        {
            var ray = transform.up * -100f * rayRotation;
            if (!Physics.Raycast(transform.position, ray, out RaycastHit hit)) return;
        
            if (!hit.collider.TryGetComponent(out Slot slot)) return;
        
            EventController.OnDataUpdate?.Invoke(slot);
            switch (slot.inventory.Type)
            {
                case InventoryType.Gold:
                    DataBase.GoldAmount += slot.inventory.Amount;
                    break;
                case InventoryType.Cash:
                    DataBase.CashAmount += slot.inventory.Amount;
                    break;
                case InventoryType.Health:
                    DataBase.HealthShot += slot.inventory.Amount;
                    break;
                case InventoryType.Adrenalin:
                    DataBase.AdrenalinShot += slot.inventory.Amount;
                    break;
                case InventoryType.Medical:
                    DataBase.MedicalKit += slot.inventory.Amount;
                    break;
                case InventoryType.Bomb:
                    EventController.OnPlayerFail?.Invoke();
                    DataBase.ResetData();
                    //TODO Restart Game and Change Slots
                    break;
            }

            EventController.OnPlayerWinReward?.Invoke();

            if (spinCount is 5 or 6 or 30 or 31)
            {
                EventController.OnPlayerReachBonus?.Invoke(spinCount);
            }
        }
    
        void ChangeState(SpinState state)
        {
            indicatorImage.sprite = state switch
            {
                SpinState.Normal => indicatorImages[0],
                SpinState.Safe => indicatorImages[1],
                SpinState.Super => indicatorImages[2],
                _ => indicatorImages[0]
            };
        }
    
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.up * -100f * rayRotation);
        }

    }
}