using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    private PlayerStandData standData;
    public PlayerStandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        standData = groundedData.StandData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.animation.SetCrouch(false);
        stateMachine.PlayerController.view.Stand();
        stateMachine.PlayerController.view.cameraLookAt.DOLocalMove(standData.CameraLookAtHeight, 0.2f);

        stateMachine.ReusableData.MovementSpeedModifier = standData.MovementSpeedModifier;
    }

    protected override void AddInputActionsCallbacks()
    {
        InputManager.playerActions.Crouch.started += OnCrouchChange;
    }

    protected override void RemoveInputAcionsCallbacks()
    {
        InputManager.playerActions.Crouch.started -= OnCrouchChange;
    }
}
