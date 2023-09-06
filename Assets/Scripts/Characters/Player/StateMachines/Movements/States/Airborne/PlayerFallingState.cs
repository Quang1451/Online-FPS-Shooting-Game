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
}
