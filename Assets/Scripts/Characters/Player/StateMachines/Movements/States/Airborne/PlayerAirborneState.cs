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

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.MovementSpeedModifier = 0f;
    }

    public override void PhysicsUpdate()
    {
        
    }

    protected override void OnContactWithGroundEnter(Collider collider)
    {
        stateMachine.ChangeState(stateMachine.StandingState);
    }
}
