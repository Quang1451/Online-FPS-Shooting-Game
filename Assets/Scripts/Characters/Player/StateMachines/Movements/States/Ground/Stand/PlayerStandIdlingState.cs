using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandIdlingState : PlayerStandingState
{
    public PlayerStandIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
        stateMachine.View.Animator.CrossFade(stateMachine.View.Idle, 0.3f);
        if (IsMovingHorizontally()) ResetVelocityHorizontally();
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.CrouchIdlingState);
        }
    }

    protected override void OnMovementPerformed(InputAction.CallbackContext context)
    {
        if (stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.CrouchMovingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.StandMovingState);
    }
}
