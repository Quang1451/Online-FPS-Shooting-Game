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
        stateMachine.View.Animator.CrossFade(stateMachine.View.Fall, 0.1f);
    }

    public override void Update()
    {
        base.Update();
        if(GetPlayerVerticalVelocity().y < -fallData.MaxFallVelocity)
        {
            stateMachine.PlayerMovement.Rigidbody.velocity = (new Vector3(GetPlayerHorizontalVelocity().x, -fallData.MaxFallVelocity, GetPlayerHorizontalVelocity().z));
        }
    }
}
