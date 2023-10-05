using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerInventory : MonoBehaviour
{
    [Header("Weapons:")]
    [SerializeField] private InventorySO DefaultInventory;
    
    [SerializeField] private Gun MainGun;
    [SerializeField] private Gun SubGun;

    [Header("Setting:")]
    [SerializeField] private Transform Transform;

    private PlayerAnimation _animation;
    private Gun _gunActive;

    private void Awake()
    {
        _animation = GetComponent<PlayerAnimation>();

        ItemType gunType = ItemType.SubGun;
        
        if(DefaultInventory!= null)
        {
            if (DefaultInventory.DefaultMainGun)
            {
                AddGun(SpawnManager.GetGun(DefaultInventory.DefaultMainGun.name));
                gunType = ItemType.MainGun;
            }
            if (DefaultInventory.DefaultSubGun) AddGun(SpawnManager.GetGun(DefaultInventory.DefaultSubGun.name));
        }
        
        ActiveGun(gunType);
    }

    private void OnEnable()
    {
        InputManager.playerActions.Button1.started += OnActiveMainGun;
        InputManager.playerActions.Button2.started += OnActiveSubGun;
    }

    
    private void OnDisable()
    {
        InputManager.playerActions.Button1.started -= OnActiveMainGun;
        InputManager.playerActions.Button2.started -= OnActiveSubGun;
    }

    public void AddGun(IItem item)
    {
        if (item == null) return;

        item.SetParent(Transform);
        SwapGun(item, item.GetItemType());
    }

    private void ActiveGun(ItemType type)
    {
        if(type == ItemType.MainGun)
        {
            MainGun?.gameObject.SetActive(true);
            SubGun?.gameObject.SetActive(false);
            _gunActive = MainGun;
        }
        else
        {
            MainGun?.gameObject.SetActive(false);
            SubGun?.gameObject.SetActive(true);
            _gunActive = SubGun;
        }

        _animation.animator.runtimeAnimatorController = _gunActive.GetAnimation();
    }

    private void SwapGun(IItem newGun, ItemType type)
    {
        IGun oldGun;
        switch (type)
        {
            case ItemType.MainGun:
                oldGun = MainGun;
                MainGun = (Gun) newGun;
                ActiveGun(ItemType.MainGun);
                break;
            default:
                oldGun = SubGun;
                SubGun = (Gun) newGun;
                break;
        }

        if (oldGun != null)
        {
            DropGun(oldGun);
        }
    }

    private void DropGun(IGun gun)
    {
        var dropItem = SpawnManager.SpawnDropItem.GetObject();
        dropItem.SetData((IItem)gun);
        dropItem.transform.position = transform.position;
    }

    public bool HasGun(ItemType type)
    {
        switch (type)
        {
            case ItemType.MainGun:
                return MainGun != null;

            default:
                return SubGun != null;
        }
    }

    public Gun GetGun(ItemType type)
    {
        switch (type)
        {
            case ItemType.MainGun:
                return MainGun;
            default:
                return SubGun;
        }
    }

    //Callback
    private void OnActiveMainGun(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if(HasGun(ItemType.MainGun) && _gunActive != MainGun)
        {
            ActiveGun(ItemType.MainGun);
        }
    }
    private void OnActiveSubGun(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (HasGun(ItemType.SubGun) && _gunActive != SubGun)
        {
            ActiveGun(ItemType.SubGun);
        }
    }
}
