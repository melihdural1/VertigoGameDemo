using System;
using System.Collections.Generic;
using System.Linq;
using Collectables;
using UnityEngine;

namespace Inventories
{
    public class InventoryManager : MonoBehaviour
    {
        public CollectableManager collectibleManager;
        public List<BaseInventory> inventories;
        public List<ImageType> images;
    }
}