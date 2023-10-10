using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCrouchingState : PlayerGroundedState
{
    protected PlayerCrouchData crouchData;
    private Tween _tween;
    public PlayerCrouchingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        crouchData = groundedData.CrouchData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.View.Crouching();
        _tween = stateMachine.View.SmoothDampAnimatorLayer(stateMachine.View.CrouchLayer, stateMachine.View.Animator.GetLayerWeight(stateMachine.View.CrouchLayer), 1);
    }

    public override void Exit()
    {
        base.Exit();
        _tween.Kill();
    }
}
