using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Data/Weapons/GunSO")]
public class GunSO : WeaponSO
{
    [field: SerializeField] public Vector2[] Recoild;
}