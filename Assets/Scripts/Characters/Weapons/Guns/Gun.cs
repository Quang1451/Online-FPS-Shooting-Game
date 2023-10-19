using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : BaseWeapon, IGun
{
    [Header("Target Rigging:")]
    public Transform LeftHolding;
    public Transform RightHolding;

    #region Main Methods
    public virtual void Aim()
    {
    }

    public virtual void Reload()
    {
    }

    public virtual void Shoot()
    {
    }
    #endregion

    #region BaseWeapon Override
    public override void AddInputAction()
    {
        InputManager.playerActions.Aim.started += OnAim;
        InputManager.playerActions.Aim.performed += OnAim;
        InputManager.playerActions.Aim.canceled += OnAim;
    }

    public override void RemoveInputAction()
    {
        
    }
    #endregion

    #region Callback Methods
    private void OnAim(InputAction.CallbackContext ctx)
    {
        
    }
    #endregion
}
