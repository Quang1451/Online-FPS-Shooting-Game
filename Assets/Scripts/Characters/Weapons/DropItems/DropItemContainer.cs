using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DropItemContainer : MonoBehaviour
{    
    private IItem Item;
    
    private MeshCollider _meshCollider;

    private void Awake()
    {
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void SetData(IItem _item)
    {
        if (_item == null) return;
        Item = _item;
        _meshCollider.sharedMesh = Item.GetMesh();
        Item.SetParent(transform);
        Item.SetVisible();
    }

    public IItem GetItem()
    {
        return Item;
    }
}

public enum ItemType
{
    MainGun,
    SubGun,
}
