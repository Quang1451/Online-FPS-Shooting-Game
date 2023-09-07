using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementSO", menuName = "Data/Character/Player")]
public class PlayerMovementSO : ScriptableObject
{   
    [field: SerializeField] public float SpeedRotation { get; private set; } = 20f;
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }
    
    [field: Header("Grouded Data:")]
    [field: SerializeField] public PlayerGroundedData GroundedData { get; private set;}

    [field: Header("Airborne Data:")]
    [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }

    public bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
    }

    public bool IsGroundLayer(int layer)
    {
        return ContainsLayer(GroundLayer, layer);
    }
}


