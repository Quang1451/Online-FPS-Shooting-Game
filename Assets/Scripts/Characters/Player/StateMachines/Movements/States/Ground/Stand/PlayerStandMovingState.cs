using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStandMovingState : PlayerStandingState
{
    public PlayerStandMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = standData.MovementSpeedModifier;
        stateMachine.View.Animator.CrossFade(stateMachine.View.Run, 0.3f);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.CrouchMovingState);
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        if (stateMachine.ReusableData.IsCrouching)
        {
            stateMachine.ChangeState(stateMachine.CrouchIdlingState);
            return;
        }
        stateMachine.ChangeState(stateMachine.StandIdlingState);
    }
}
