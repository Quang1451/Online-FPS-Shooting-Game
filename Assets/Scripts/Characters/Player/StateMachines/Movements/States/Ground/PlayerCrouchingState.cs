using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchingState : PlayerGroundedState
{
    public PlayerCrouchingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 0.5f;
    }
}
