using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchIdlingState : PlayerCrouchingState
{
    public PlayerCrouchIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        stateMachine.View.animationUtility.Animator.CrossFade(stateMachine.View.animationUtility.Idle, 0.3f);
        if (IsMovingHorizontally()) ResetVelocityHorizontally();
    }

    public override void Update()
    {
        base.Update();
        if(!stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.StandIdlingState);
        }
    }

    protected override void OnMovementPerformed(InputAction.CallbackContext context)
    {
        if (stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.StandMovingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.CrouchMovingState);
    }
}
