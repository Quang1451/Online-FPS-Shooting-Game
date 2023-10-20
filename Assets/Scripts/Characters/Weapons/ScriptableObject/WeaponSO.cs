using UnityEngine;

public class WeaponSO : ScriptableObject
{
    [Header("Animator:")]
    [field: SerializeField] public AnimatorOverrideController Animator;

    [Header("Settings:")]
    [field: SerializeField] public ItemType Type;
    [field: SerializeField] [field: Range(0f, 100f)] public float MaxDamage = 100f;
    [field: SerializeField] [field: Range(0f, 100f)] public float MinDamage = 0f;
}



