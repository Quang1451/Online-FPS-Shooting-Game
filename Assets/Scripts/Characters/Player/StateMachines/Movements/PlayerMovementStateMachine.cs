using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerController PlayerController { get; }
    public PlayerStateReusubleData ReusableData { get; }

    public PlayerStandingState StandingState { get; }
    public PlayerCrouchingState CrouchingState { get; }
    public PlayerMovementStateMachine(PlayerController playerController)
    {
        PlayerController = playerController;
        ReusableData = PlayerController.reusubleData;

        StandingState = new PlayerStandingState(this);
        CrouchingState = new PlayerCrouchingState(this);
    }
}
