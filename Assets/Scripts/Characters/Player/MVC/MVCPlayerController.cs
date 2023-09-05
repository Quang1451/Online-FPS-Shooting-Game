using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerController : BaseController
{
    public MVCPlayerModel model => (MVCPlayerModel)_data;
    public MVCPlayerView view => (MVCPlayerView)_view;

    public PlayerAnimation animation;
    public PlayerMovement movement;
    public override void Initialize()
    {
        base.Initialize();

        _view.SpawnModel(OnLoadModelComplete);
        _data.ApplyDesign(OnApplyDesign);
    }

    private void OnLoadModelComplete()
    {
    }

    private void OnApplyDesign()
    {
        animation = _view.GetComponent<PlayerAnimation>();
        movement = _view.GetComponent<PlayerMovement>();

        movement.Initalize(new PlayerMovementData
        {
            reusubleData = model.reusubleData
        });

        animation.SetArm();
    }

    public override void Update()
    {
        movement?.DoUpdate();
    }

    public override void FixedUpdate()
    {
        movement?.DoPhysicUpdate();
    }
}
