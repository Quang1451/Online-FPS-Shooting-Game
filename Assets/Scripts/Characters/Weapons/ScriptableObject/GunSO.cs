using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunSO", menuName = "Data/Weapons/GunSO")]
public class GunSO : WeaponSO
{
    [field: SerializeField] public Vector2[] Recoild;

    [field: SerializeField] public float DelayShot = 1f;
    [field: SerializeField] public bool CanContinuousShot = false;
    [field: SerializeField] public bool CycleReload = false;
    [field: SerializeField] public int Magazine = 10;
}