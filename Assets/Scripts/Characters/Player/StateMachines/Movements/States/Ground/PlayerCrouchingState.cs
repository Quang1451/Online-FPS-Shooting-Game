using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCrouchingState : PlayerGroundedState
{
    protected PlayerCrouchData crouchData;
    public PlayerCrouchingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        crouchData = groundedData.CrouchData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerMovement.PlayerAnimation.SetCrouch(true);
        stateMachine.PlayerMovement.View.Crouch();
        stateMachine.PlayerMovement.View.cameraLookAt.DOLocalMove(crouchData.CameraLookAtHeight, 0.2f);

        stateMachine.ReusableData.MovementSpeedModifier = crouchData.MovementSpeedModifier;
    }

    protected override void AddInputActionsCallbacks()
    {
        InputManager.playerActions.Crouch.canceled += OnCrouchChange;
    }

    protected override void RemoveInputAcionsCallbacks()
    {
        InputManager.playerActions.Crouch.canceled -= OnCrouchChange;
    }
}
