using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapons:")]
    [SerializeField] private GameObject MainGun;
    [SerializeField] private GameObject SubGun;

    [Header("Setting:")]
    [SerializeField] private Transform Transform;

    public void AddGun(GameObject obj)
    {
        obj.transform.SetParent(Transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        obj.transform.localScale = Vector3.one;
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

    public GameObject GetGun(ItemType type)
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
