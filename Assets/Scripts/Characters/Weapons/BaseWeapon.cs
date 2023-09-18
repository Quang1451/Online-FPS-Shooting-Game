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
    }

    public virtual void Unequip()
    {
        RemoveInputAction();
    }

    
    public virtual void AddInputAction()
    {
    }

    public virtual void RemoveInputAction()
    {
    }

    
}
