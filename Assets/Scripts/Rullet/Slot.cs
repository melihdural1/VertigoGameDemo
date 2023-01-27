using Collectables;
using Controllers;
using UnityEngine;

namespace Rullet
{
    public class Slot : MonoBehaviour
    {
        public SpinRullet parent;
        public SlotType slotType;
        public Inventories.Inventory inventory;

        private void Awake()
        {
            parent = GetComponentInParent<SpinRullet>();
            inventory = GetComponentInChildren<Inventories.Inventory>();
            EventController.OnSpinStart += SetRotation;
        }

        private void OnDestroy()
        {
            EventController.OnSpinStart -= SetRotation;
        }
        

        void SetRotation(float zAxis)
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.Abs(zAxis));
        }
    }

    public enum SlotType
    {
        Reward,
        Bomb
    }
}