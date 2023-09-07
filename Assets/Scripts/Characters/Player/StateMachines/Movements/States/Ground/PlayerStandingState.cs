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
        stateMachine.PlayerMovement.PlayerAnimation.CrossFade(stateMachine.PlayerMovement.PlayerAnimation.Stand, 0.2f);

        stateMachine.PlayerMovement.View.Stand();
        stateMachine.PlayerMovement.View.cameraLookAt.DOLocalMove(standData.CameraLookAtHeight, 0.2f);

        stateMachine.ReusableData.MovementSpeedModifier = standData.MovementSpeedModifier;
    }

    protected override void AddInputActionsCallbacks()
    {
        InputManager.playerActions.Crouch.started += OnCrouchChange;
        InputManager.playerActions.Jump.started += OnJumpChange;
    }

    protected override void RemoveInputAcionsCallbacks()
    {
        InputManager.playerActions.Crouch.started -= OnCrouchChange;
        InputManager.playerActions.Jump.started -= OnJumpChange;
    }

    private void OnJumpChange(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        stateMachine.ChangeState(stateMachine.JumpingState);
    }
}