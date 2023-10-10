using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    public PlayerMovement PlayerMovement { get; }
    public MVCPlayerView View { get;}
    public PlayerStateReusubleData ReusableData { get; }

    //Grounded
    //  Standing
    public PlayerStandIdlingState StandIdlingState { get; }
    public PlayerStandMovingState StandMovingState { get; }
    //  Crouching
    public PlayerCrouchIdlingState CrouchIdlingState { get; }
    public PlayerCrouchMovingState CrouchMovingState { get; }
    //Airbone
    public PlayerJumpingState JumpingState { get; }
    public PlayerFallingState FallingState { get; }

    public PlayerMovementStateMachine(PlayerMovement playerMovement)
    {
        PlayerMovement = playerMovement;
        View = playerMovement.View;
        ReusableData = PlayerMovement.Data.reusubleData;

        StandIdlingState = new PlayerStandIdlingState(this);
        StandMovingState = new PlayerStandMovingState(this);

        CrouchIdlingState = new PlayerCrouchIdlingState(this);
        CrouchMovingState = new PlayerCrouchMovingState(this);

        JumpingState = new PlayerJumpingState(this);
        FallingState = new PlayerFallingState(this);
    }
}
