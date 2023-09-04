using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerCrouchData
{
    [field: SerializeField] [field: Range(0.3f, 0.5f)] public float MovementSpeedModifier { get; private set; } = 0.5f;
    [field: SerializeField] public Vector3 CameraLookAtHeight = new Vector3(0, -1, 0);

}