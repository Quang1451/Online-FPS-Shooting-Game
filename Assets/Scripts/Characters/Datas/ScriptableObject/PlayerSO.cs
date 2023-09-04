using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "Data/Character/Player")]
public class PlayerSO : ScriptableObject
{   
    [field: SerializeField] public float SpeedRotation { get; private set; } = 20f;
    [field: SerializeField] public LayerMask GoundedLayer { get; private set; }

    [field: Header("Grouded Data:")]
    [field: SerializeField] public PlayerGroundedData GroundedData { get; private set;}
}
