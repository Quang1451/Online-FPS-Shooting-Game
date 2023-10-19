using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : IItem
{
    void Initiazlie();
    void Equip();
    void Unequip();
    void AddInputAction();
    void RemoveInputAction();
    AnimatorOverrideController GetWeaponAnimator();
}

public interface IGun
{
    void Shoot();
    void Reload();
    void Aim();
}

public interface IItem
{
    void SetParent(Transform parent);
    void SetVisible(bool value = true);
    ItemType GetItemType();
    Mesh GetMesh();
}