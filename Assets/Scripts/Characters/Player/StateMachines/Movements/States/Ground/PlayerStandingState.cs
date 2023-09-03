using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandingState : PlayerGroundedState
{
    public PlayerStandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ReusableData.MovementSpeedModifier = 1f;
    }
}
