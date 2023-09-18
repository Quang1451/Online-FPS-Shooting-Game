using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPickUpSO", menuName ="Data/Player/Pick Up")]
public class PlayerPickUpSO : ScriptableObject
{
    [SerializeField] public LayerMask PickUpLayer;

    public bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
    }

    public bool IsPickUpLayer(int layer)
    {
        return ContainsLayer(PickUpLayer, layer);
    }
}
