using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerMovement PlayerMovement { get; }
    public PlayerStateReusubleData ReusableData { get; }

    public PlayerStandingState StandingState { get; }
    public PlayerCrouchingState CrouchingState { get; }
    public PlayerMovementStateMachine(PlayerMovement playerMovement)
    {
        PlayerMovement = playerMovement;
        ReusableData = PlayerMovement.Data.reusubleData;

        StandingState = new PlayerStandingState(this);
        CrouchingState = new PlayerCrouchingState(this);
    }
}
