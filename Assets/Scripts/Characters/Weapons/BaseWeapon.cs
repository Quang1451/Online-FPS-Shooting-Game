using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected WeaponSO WeaponSO;

    public virtual void Initiazlie()
    {
    }

    public virtual void Equip()
    {
        AddInputAction();
        SetVisible();
    }

    public virtual void Unequip()
    {
        RemoveInputAction();
        SetVisible(false);
    }

    public virtual void AddInputAction()
    {
    }

    public virtual void RemoveInputAction()
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
