using System;
using UnityEngine;

[Serializable]
public class PlayerFallData
{
    [field: SerializeField] [field: Range(5f, 15f)] public float MaxFallVelocity { get; private set; } = 10f;
}