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
        _data.ApplyDesign();

        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.StandingState);

        animation = _view.GetComponent<PlayerAnimation>();

        animation.SetArm();
    }

    private void OnLoadModelComplete()
    {
    }

    public override void Update()
    {
        movementStateMachine.Update();
        movementStateMachine.HandleInput();

        view.Rotate();
    }

    public override void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
}
