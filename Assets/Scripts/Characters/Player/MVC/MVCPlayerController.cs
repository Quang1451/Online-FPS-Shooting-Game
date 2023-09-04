using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerController : BaseController
{
    public MVCPlayerModel model => (MVCPlayerModel)_data;
    public MVCPlayerView view => (MVCPlayerView)_view;


    public PlayerMovementStateMachine movementStateMachine;
    public PlayerAnimation animation;
    
    public override void Initialize()
    {
        base.Initialize();
        _view.SpawnModel(OnLoadModelComplete);
        _data.ApplyDesign(OnApplyDesign);

        animation = _view.GetComponent<PlayerAnimation>();
    }

    private void OnLoadModelComplete()
    {
    }

    private void OnApplyDesign()
    {
        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.StandingState);
        animation.SetArm();
    }

    public override void Update()
    {
        movementStateMachine?.Update();
        movementStateMachine?.HandleInput();
        
        if (model.playerSO == null) return;
        view.Rotate(model.playerSO.SpeedRotation);
    }

    public override void FixedUpdate()
    {
        movementStateMachine?.PhysicsUpdate();
    }
}
