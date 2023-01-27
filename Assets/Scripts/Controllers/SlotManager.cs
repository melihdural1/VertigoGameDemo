using System;
using System.Collections.Generic;
using Rullet;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class SlotManager : MonoBehaviour
    {
        public List<Slot> slots;

        private void Awake()
        {
            var randomIndex = Random.Range(0, slots.Count - 1);
            slots[randomIndex].slotType = SlotType.Bomb;
            
            foreach (var slot in slots)
            {
                if (slot != slots[randomIndex])
                {
                    slot.slotType = SlotType.Reward;
                }
            }
            
        }
    }
}