using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public MVCPlayerController PlayerController { get; }
    public PlayerStateReusubleData ReusableData { get; }

    public PlayerStandingState StandingState { get; }
    public PlayerCrouchingState CrouchingState { get; }
    public PlayerMovementStateMachine(MVCPlayerController playerController)
    {
        PlayerController = playerController;
        ReusableData = PlayerController.model.reusubleData;

        StandingState = new PlayerStandingState(this);
        CrouchingState = new PlayerCrouchingState(this);
    }
}
