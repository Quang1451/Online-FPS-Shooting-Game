using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Initiazlie();
    void Equip();
    void Unequip();
    void AddInputAction();
    void RemoveInputAction();
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
    ItemType GetItemType();

    Mesh GetMesh();
}