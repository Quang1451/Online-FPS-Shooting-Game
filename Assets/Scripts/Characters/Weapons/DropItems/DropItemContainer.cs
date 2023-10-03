using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemContainer : MonoBehaviour
{    
    public IItem Item;
    public ItemType Type;

    public void SetData(IItem obj)
    {
        if (obj == null) return;
        Type = obj.GetItemType();
        obj.SetParent(transform);
        obj.EnableCollider();
    }
}

public enum ItemType
{
    MainGun,
    SubGun,
}
