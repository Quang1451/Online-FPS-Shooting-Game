using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DropItemContainer : MonoBehaviour
{    
    public IItem Item;
    public ItemType Type;

    private MeshCollider _meshCollider;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }


    public void SetData(IItem _item)
    {
        if (_item == null) return;
        Item = _item;
        Type = Item.GetItemType();
        _meshCollider.sharedMesh = Item.GetMesh();
        _item.SetParent(transform);
    }
}

public enum ItemType
{
    MainGun,
    SubGun,
}
