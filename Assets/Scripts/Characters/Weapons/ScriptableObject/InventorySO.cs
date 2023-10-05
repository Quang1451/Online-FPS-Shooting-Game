using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Data/Inventory")]
public class InventorySO : ScriptableObject
{
    public Gun DefaultMainGun;
    public Gun DefaultSubGun;
}
