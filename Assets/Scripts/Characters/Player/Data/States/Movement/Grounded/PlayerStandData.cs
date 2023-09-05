using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStandData 
{
    [field: SerializeField] [field: Range(0.8f, 1f)] public float MovementSpeedModifier { get; private set; } = 1f;
    [field: SerializeField] public Vector3 CameraLookAtHeight = new Vector3(0, 0, 0);
}

