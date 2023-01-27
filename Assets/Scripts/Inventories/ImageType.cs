using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageType : MonoBehaviour
{
    public InventoryType InventoryType;
    public Sprite imageSprite;

    public InventoryType Type
    {
        get => InventoryType;
        set => InventoryType = value;
    }
}

public enum InventoryType
{
    Gold,
    Cash,
    Health,
    Adrenalin,
    Medical,
    Bomb
}
