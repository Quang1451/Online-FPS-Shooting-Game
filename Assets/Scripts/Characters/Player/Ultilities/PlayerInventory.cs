using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapons:")]
    [SerializeField] private IGun MainGun;
    [SerializeField] private IGun SubGun;

    [Header("Setting:")]
    [SerializeField] private Transform Transform;

    public void AddGun(IItem item)
    {
        item.SetParent(Transform);

        SwapGun(item, item.GetItemType());
    }

    private void SwapGun(IItem newGun, ItemType type)
    {
        IGun oldGun;
        switch (type)
        {
            case ItemType.MainGun:
                oldGun = MainGun;
                MainGun = (IGun) newGun;
                break;
            default:
                oldGun = SubGun;
                SubGun = (IGun) newGun;
                break;
        }

        if (oldGun != null)
        {
            var dropItem = SpawnManager.SpawnDropItem.GetObject();
            dropItem.SetData((IItem) oldGun);
            dropItem.transform.position = transform.position;
        }
    }

    public bool HasGun(ItemType type)
    {
        switch (type)
        {
            case ItemType.MainGun:
                return MainGun != null;

            default:
                return SubGun != null;
        }
    }

    public IGun GetGun(ItemType type)
    {
        switch (type)
        {
            case ItemType.MainGun:
                return MainGun;
            default:
                return SubGun;
        }
    }
}
