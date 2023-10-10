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
        stateMachine.View.Animator.CrossFade(stateMachine.View.Run, 0.3f);
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
