using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected WeaponSO WeaponSO;
    
    [Header("Target Rigging:")]
    [SerializeField] private Transform LeftHolding;
    [SerializeField] private Transform RightHolding;

    protected PlayerInventory _inventory;
    public virtual void Initiazlie()
    {
    }

    public virtual void Equip(IData data)
    {
        _inventory = ((WeaponData) data).inventory;
        _inventory.View.animationUtility.SetWeaponHandTarget(LeftHolding, RightHolding);
        AddInputAction();
        SetVisible();
    }

    public virtual void Unequip()
    {
        _inventory = null;
        RemoveInputAction();
        SetVisible(false);
    }

    public virtual void AddInputAction()
    {
    }

    public virtual void RemoveInputAction()
    {
    }

    public virtual void DoUpdate()
    {
    }

    public virtual ItemType GetItemType()
    {
        return WeaponSO.Type;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    public Mesh GetMesh()
    {
        return GetComponentInChildren<MeshFilter>().sharedMesh;
    }
    
    public AnimatorOverrideController GetWeaponAnimator()
    {
        return WeaponSO.Animator;
    }

    public void SetVisible(bool value = true)
    {
        gameObject.SetActive(value);
    }

   
}

public class WeaponData : IData
{
    public PlayerInventory inventory;
}