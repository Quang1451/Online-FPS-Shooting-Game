using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCrouchMovingState : PlayerCrouchingState
{
    public PlayerCrouchMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = crouchData.MovementSpeedModifier;
        if (stateMachine.ReusableData.IsAiming)
        {
            stateMachine.View.Animator.CrossFade(stateMachine.View.Step, 0.1f);
            return;
        }
        stateMachine.View.Animator.CrossFade(stateMachine.View.Run, 0.1f);
    }

    public override void Update()
    {
        base.Update();
        if (!stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.StandMovingState);
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (!stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.StandIdlingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.CrouchIdlingState);
    }
}
