using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerController : BaseController
{
    public MVCPlayerModel model => (MVCPlayerModel)_data;
    public MVCPlayerView view => (MVCPlayerView)_view;

    public PlayerMovement movement;
    public PlayerInventory inventory;

    public override void Initialize()
    {
        base.Initialize();
        _view.SpawnModel(OnLoadModelComplete);
    }

    private void OnLoadModelComplete()
    {
        movement = _view.GetComponent<PlayerMovement>();
        inventory = _view.GetComponent<PlayerInventory>();

        movement.Initialize(new MovementData
        {
            reusubleData = model.reusubleData
        });

        inventory.Initialize(new InventoryData{
            reusubleData = model.reusubleData
        });
    }

    public override void Update()
    {
        _view.DoUpdate();
        movement?.DoUpdate();
        inventory?.DoUpdate();
    }

    public override void FixedUpdate()
    {
        movement?.DoPhysicUpdate();
    }
}
