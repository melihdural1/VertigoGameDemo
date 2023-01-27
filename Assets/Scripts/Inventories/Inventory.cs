using System.Linq;
using System.Threading.Tasks;
using Controllers;
using Random = UnityEngine.Random;

namespace Inventories
{
    public class Inventory : BaseInventory
    {
        public override void Awake()
        {
            base.Awake();
            SelectInventory();
            
            EventController.OnPlayerWinReward += UpdateRewardAmount;
            EventController.OnPlayerReachBonus += ResetInventories;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            EventController.OnPlayerWinReward -= UpdateRewardAmount;
            EventController.OnPlayerReachBonus -= ResetInventories;
        }

        protected override void SelectInventory()
        {
            if (Type == InventoryType.Bomb)
            {
                var images = InventoryManager.images.Where(x=> x.Type == InventoryType.Bomb).ToList();
                
                InventoryImage = images[Random.Range(0, images.Count - 1)];
            
                SpriteSlot.sprite = InventoryImage.imageSprite;

                Amount = 1;
                amountText.text = "x" + Amount;
            }
            else
            {
                SpriteSlot.sprite  = InventoryManager.images.FirstOrDefault(x => x.Type == Type)?.imageSprite;

                Amount = Random.Range(1, 101);
                amountText.text = "x" + Amount;
            }
            EventController.OnInventoriesSubscribeToList?.Invoke(this);
        }

        public override void ResetInventories(int turnCount)
        {
            base.ResetInventories(turnCount);
            SelectInventory();
        }

        async void UpdateRewardAmount()
        {
            await Task.Delay(400);
            if (Type == InventoryType.Bomb) return;

            Amount *= 2;
            amountText.text = "x" + Amount;
        }
    }
}