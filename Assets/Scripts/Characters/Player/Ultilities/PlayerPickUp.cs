using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerPickUp : MonoBehaviour
{
    [Header("Pick Up Setting:")]
    [SerializeField] private LayerMask Layer;
    [SerializeField] private bool AutoPickUp = true;


    private List<GameObject> _pickUpList;
    private PlayerInventory _inventory;

    private void Awake()
    {
        _pickUpList = new List<GameObject>();
        _inventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        InputManager.playerActions.PickUp.performed += PickUpPerformed;
    }

    private void OnDisable()
    {
        InputManager.playerActions.PickUp.performed -= PickUpPerformed;
    }

    private void PickUpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_pickUpList == null || _pickUpList.Count == 0) return;

        PickUpHandle(_pickUpList[0]);
    }

    private void PickUpHandle(GameObject obj)
    {
        var item = obj;
        var dropItem = item.GetComponent<DropItemContainer>();
        _inventory.AddGun(dropItem.Item);

        _pickUpList.Remove(item);

        SpawnManager.SpawnDropItem.ReleaseObject(dropItem);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (!IsPickUpLayer(obj.layer)) return;

        if (AutoPickUp && !_inventory.HasGun(obj.GetComponent<DropItemContainer>().Type))
        {
            PickUpHandle(obj);
            return;
        }

        _pickUpList.Add(obj);
        Debug.Log("Pickup " + obj.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_pickUpList.Contains(other.gameObject))
        {
            _pickUpList.Remove(other.gameObject);
        }
    }

    public bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
    }

    public bool IsPickUpLayer(int layer)
    {
        return ContainsLayer(Layer, layer);
    }
}
