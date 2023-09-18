using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Data/Weapons/BaseWeapon")]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField] [field: Range(0f, 100f)] public float MaxDamage = 100f;
    [field: SerializeField] [field: Range(0f, 100f)] public float MinDamage = 0f;
}
