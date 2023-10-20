using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IItem
{
    void Initiazlie();
    void Equip(IData data);
    void Unequip();
    void AddInputAction();
    void RemoveInputAction();

    void DoUpdate();
    
    AnimatorOverrideController GetWeaponAnimator();
}

public interface IGun
{
    void Shoot();
    void Reload();
    void Aim(bool value);
    void OnReloadFinish();
}

public interface IItem
{
    void SetParent(Transform parent);
    void SetVisible(bool value = true);
    ItemType GetItemType();
    Mesh GetMesh();
}