using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerPickUp : MonoBehaviour
{
    [Header("Pick Up Setting:")]
    [SerializeField] public LayerMask Layer;

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
        var item = _pickUpList[0];
        
        _inventory.AddGun(item.GetComponent<DropItemContainer>().Object);
        _pickUpList.Remove(item);
        
        item.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPickUpLayer(other.gameObject.layer))
        {
            _pickUpList.Add(other.gameObject);
            Debug.Log("Pickup " + other.name);
        }
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
