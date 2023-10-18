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
        _tween = stateMachine.View.animationUtility.SmoothDampAnimatorLayer(stateMachine.View.animationUtility.CrouchLayer,
            stateMachine.View.animationUtility.Animator.GetLayerWeight(stateMachine.View.animationUtility.CrouchLayer), 1);
    }

    public override void Exit()
    {
        base.Exit();
        _tween.Kill();
    }
}
