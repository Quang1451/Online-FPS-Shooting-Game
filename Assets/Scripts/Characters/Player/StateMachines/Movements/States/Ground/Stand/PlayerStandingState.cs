using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    protected PlayerStandData standData;
    private Tween _tween;
    public PlayerStandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        standData = groundedData.StandData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.View.Standing();
        _tween = stateMachine.View.SmoothDampAnimatorLayer(stateMachine.View.CrouchLayer, stateMachine.View.Animator.GetLayerWeight(stateMachine.View.CrouchLayer), 0);
    }

    public override void Exit()
    {
        base.Exit();
        _tween.Kill();
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        InputManager.playerActions.Jump.started += OnJumpChange;
    }

    protected override void RemoveInputAcionsCallbacks()
    {
        base.RemoveInputAcionsCallbacks();
        InputManager.playerActions.Jump.started -= OnJumpChange;
    }

    private void OnJumpChange(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (stateMachine.ReusableData.JumpDelayTime > Time.time) return;
        stateMachine.ChangeState(stateMachine.JumpingState);
    }
}
