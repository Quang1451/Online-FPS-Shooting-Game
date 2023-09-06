using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirborneState : PlayerMovementState
{
    protected PlayerAirborneData airborneData;
    public PlayerAirborneState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        airborneData = stateMachine.PlayerMovement.MovementSO.AirborneData;
    }
}
