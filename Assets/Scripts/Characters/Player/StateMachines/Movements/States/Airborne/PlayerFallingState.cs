using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{
    private PlayerFallData fallData;
    public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        fallData = airborneData.FallData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerMovement.PlayerAnimation.CrossFade(stateMachine.PlayerMovement.PlayerAnimation.Fall, 0.1f);

    }
}
