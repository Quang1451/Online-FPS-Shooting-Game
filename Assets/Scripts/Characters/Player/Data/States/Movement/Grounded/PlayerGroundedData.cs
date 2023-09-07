using System;
using UnityEngine;

[Serializable]
public class PlayerGroundedData
{
    [field: SerializeField] [field: Range(0f, 10f)] public float BaseSpeed { get; private set; } = 5f;
    [field: SerializeField] [field: Range(0f, 5f)] public float GroundToFallRayDistance { get; private set; } = 2f;

    [field: SerializeField] public PlayerStandData StandData { get; private set; }

    [field: SerializeField] public PlayerCrouchData CrouchData { get; private set; }
}