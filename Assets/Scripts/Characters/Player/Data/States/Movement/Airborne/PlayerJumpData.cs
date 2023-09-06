using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerJumpData
{
    [field: SerializeField] public Vector3 IdleJumpForce { get; private set; } = new Vector3(0f, 5f, 0f);
    [field: SerializeField] public Vector3 MoveJumpForce { get; private set; } = new Vector3(0.5f, 4f, 0.5f);
}