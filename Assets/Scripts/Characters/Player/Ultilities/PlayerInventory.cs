using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [Header("WeaponSO:")]
    [SerializeField] private InventorySO DefaultInventory;

    [Header("Pick Up Setting:")]
    [SerializeField] private LayerMask Layer;
    [SerializeField] private Transform WeaponInventoryTransform;
     

    private List<IWeapon> _weaponsList;
    private IWeapon _weaponActive;


    private List<DropItemContainer> _dropItemList;


    private PlayerStateReusubleData _reusubleData;

    
    public void Initialize(IData data)
    {
        InventoryData inventoryData = (InventoryData)data;
        _reusubleData = inventoryData.reusubleData;
        _dropItemList = new List<DropItemContainer>();
        _weaponsList = new List<IWeapon>() { null, null};
    }


    #region Unity
    /*private void Awake()
    {
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
    */

    private void OnEnable()
    {
        InputManager.playerActions.Button1.started += OnActiveMainGun;
        InputManager.playerActions.Button2.started += OnActiveSubGun;

        InputManager.playerActions.PickUp.performed += PickUp;
    }

    private void OnDisable()
    {
        InputManager.playerActions.Button1.started -= OnActiveMainGun;
        InputManager.playerActions.Button2.started -= OnActiveSubGun;

        InputManager.playerActions.PickUp.performed -= PickUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        DropItemContainer container = other.gameObject.GetComponent<DropItemContainer>();
        if (container == null || !IsPickUpLayer(container.gameObject.layer)) return;
 
        _dropItemList.Add(container);
    }

    private void OnTriggerExit(Collider other)
    {
        DropItemContainer container = other.gameObject.GetComponent<DropItemContainer>();
        if (container == null || !IsPickUpLayer(container.gameObject.layer)) return;
        
        if (_dropItemList.Contains(container))
        {
            _dropItemList.Remove(container);
        }
    }
    #endregion

    #region Main Methods
    private void PickUpHandle(IItem item)
    {
        if (item == null) return;
        
        if(item is IWeapon)
        {
            AddWeapon((IWeapon) item);
            return;
        }
    }
    public void AddWeapon(IWeapon weapon)
    {
        if (weapon == null) return;
        weapon.SetParent(WeaponInventoryTransform);
        CheckAndSwapWeapon(weapon, weapon.GetItemType());
    }

    private void ActiveWeapon(ItemType type)
    {
        /*if(type == ItemType.MainGun)
        {
            MainGun?.gameObject.SetActive(true);
            SubGun?.gameObject.SetActive(false);
            _weaponActive = MainGun;
        }
        else
        {
            MainGun?.gameObject.SetActive(false);
            SubGun?.gameObject.SetActive(true);
            _weaponActive = SubGun;
        }*/

        /*_animation.animator.runtimeAnimatorController = _gunActive.GetAnimation();*/
    }

    private void CheckAndSwapWeapon(IWeapon newWeapon, ItemType type)
    {
        IWeapon oldWeapon = null;
        switch (type)
        {
            case ItemType.MainGun:
                oldWeapon = _weaponsList[0];
                _weaponsList[0] = newWeapon;
                break;
            default:
                oldWeapon = _weaponsList[1];
                _weaponsList[1] = newWeapon;
                break;
        }

        ActiveWeapon(type);
        DropGun(oldWeapon);
    }

    private void DropGun(IWeapon weapon)
    {
        if (weapon == null) return;
        var dropItem = SpawnManager.SpawnDropItem.GetObject();
        dropItem.SetData(weapon);
        dropItem.transform.position = transform.position;
    }

    private bool ContainsLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
    }

    private bool IsPickUpLayer(int layer)
    {
        return ContainsLayer(Layer, layer);
    }
    #endregion

    #region Callback Method
    private void OnActiveMainGun(InputAction.CallbackContext ctx)
    {
        /*if(HasGun(ItemType.MainGun) && _weaponActive != MainGun)
        {
            ActiveGun(ItemType.MainGun);
        }*/
    }

    private void OnActiveSubGun(InputAction.CallbackContext ctx)
    {
        /*if (HasGun(ItemType.SubGun) && _weaponActive != SubGun)
        {
            ActiveGun(ItemType.SubGun);
        }*/
    }

    private void PickUp(InputAction.CallbackContext ctx)
    {
        if (_dropItemList.Count < 1) return;
        DropItemContainer container = _dropItemList[0];
        PickUpHandle(container.GetItem());
        _dropItemList.Remove(container);

        SpawnManager.SpawnDropItem.ReleaseObject(container);
    }
    #endregion
}

public class InventoryData : IData
{
    public PlayerStateReusubleData reusubleData;
}