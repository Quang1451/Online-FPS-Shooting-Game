using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : BaseWeapon, IGun
{
    [Header("Target Rigging:")]
    public Transform LeftHolding;
    public Transform RightHolding;

    public virtual void Aim()
    {
    }

    public virtual void Reload()
    {
    }

    public virtual void Shoot()
    {
    }

    public override void AddInputAction()
    {

    }
}
