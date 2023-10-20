using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : BaseWeapon, IGun
{
    private GunSO _gunSO;
    private float _timeDelayShoot;
    private bool _isContinuousShot;
    private bool _isRealoading;
    private bool _isAiming;

    public int _bullet;

    #region Main Methods
    public virtual void Aim(bool value)
    {
        _inventory.View.ActiveAiming?.Invoke(value);
    }

    public virtual void Reload()
    {
        if (_isRealoading) return;
        _inventory.View.animationUtility.RigWeaponAmingChange(false);

        _isRealoading = true;
        if (_bullet == 0)
        {
            _inventory.View.animationUtility.Animator.SetTrigger(_inventory.View.animationUtility.ReloadEmptyTrigger);
            return;
        }
        _inventory.View.animationUtility.Animator.SetTrigger(_inventory.View.animationUtility.ReloadLeftTrigger);
    }

    public virtual void Shoot()
    {
        if (_timeDelayShoot > Time.time || _bullet <=0) return;
        //Shoot
        _bullet--;

        _inventory.View.animationUtility.Animator.SetTrigger(_inventory.View.animationUtility.ShootTrigger);
        _timeDelayShoot = Time.time + _gunSO.DelayShot;
    }
    #endregion

    #region BaseWeapon Override
    public override void Initiazlie()
    {
        base.Initiazlie();
        _gunSO = (GunSO)WeaponSO;

        _bullet = _gunSO.Magazine;
        _timeDelayShoot = Time.time;
    }
    public override void DoUpdate()
    {
        base.DoUpdate();
        if (_isContinuousShot)
        {
            Shoot();
        }
    }

    public override void AddInputAction()
    {
        InputManager.playerActions.Aim.started += OnAim;
        InputManager.playerActions.Aim.canceled += OnAim;

        InputManager.playerActions.Fire.started += OnFire;
        InputManager.playerActions.Fire.canceled += OnFire;

        InputManager.playerActions.Reload.started += OnReload;
    }

    public override void RemoveInputAction()
    {
        InputManager.playerActions.Aim.started -= OnAim;
        InputManager.playerActions.Aim.canceled -= OnAim;

        InputManager.playerActions.Fire.started -= OnFire;
        InputManager.playerActions.Fire.canceled -= OnFire;


        InputManager.playerActions.Reload.started -= OnReload;
    }
    #endregion

    #region Callback Methods
    private void OnAim(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Started:
                Aim(true);
                _isAiming = true;
                break;
            case InputActionPhase.Canceled:
                Aim(false);
                _isAiming = false;
                break;
        }
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {

        switch (ctx.phase)
        {
            case InputActionPhase.Started:
                Aim(true);
                if (_gunSO.CanContinuousShot)
                {
                    _isContinuousShot = true;
                }
                else
                {
                    Shoot();
                }
                return;
            case InputActionPhase.Canceled:
                if(!_isAiming) Aim(false);
                _isContinuousShot = false;
                return;
        }
    }

    private void OnReload(InputAction.CallbackContext ctx)
    {
        Reload();
    }

    public void OnReloadFinish()
    {
        _bullet = _gunSO.Magazine;
        _isRealoading = false;
        if(_isAiming) _inventory.View.animationUtility.RigWeaponAmingChange(true);
    }
    #endregion
}
