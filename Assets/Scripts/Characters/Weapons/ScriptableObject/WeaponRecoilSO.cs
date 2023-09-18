using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecoilSO", menuName = "Data/Weapons/RecoilSO")]
public class WeaponRecoilSO : ScriptableObject
{
    [SerializeField] public Vector2[] Recoils;
}
