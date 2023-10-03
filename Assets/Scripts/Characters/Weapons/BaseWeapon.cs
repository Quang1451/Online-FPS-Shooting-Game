using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon, IItem
{
    [SerializeField] protected WeaponSO WeaponSO;

    private MeshCollider _mesh;

    private void Awake()
    {
        _mesh = GetComponentInChildren<MeshCollider>(true);
    }

    public virtual void Initiazlie()
    {
    }

    public virtual void Equip()
    {
        AddInputAction();
    }

    public virtual void Unequip()
    {
        RemoveInputAction();
    }

    
    public virtual void AddInputAction()
    {
    }

    public virtual void RemoveInputAction()
    {
    }

    public virtual ItemType GetItemType()
    {
        return WeaponSO.Type;
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    public void EnableCollider()
    {
        _mesh.enabled = true;
    }

    public void DisableCollider()
    {
        _mesh.enabled = false;
    }
}
