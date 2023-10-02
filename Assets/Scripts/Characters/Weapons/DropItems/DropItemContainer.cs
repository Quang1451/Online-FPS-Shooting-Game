using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class DropItemContainer : MonoBehaviour
{    
    public GameObject Object;
    public ItemType Type;

    private MeshCollider _collider;

    private void Start()
    {
        _collider = GetComponent<MeshCollider>();
    }

    public void SetData(GameObject obj)
    {
        if (obj == null) return;
        _collider.sharedMesh = obj.GetComponent<MeshFilter>().sharedMesh;
    }
}

public enum ItemType
{
    MainGun,
    SubGun,
}
