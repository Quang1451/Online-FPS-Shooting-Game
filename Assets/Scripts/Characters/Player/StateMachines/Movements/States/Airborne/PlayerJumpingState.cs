using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerAirborneState
{
    private PlayerJumpData jumpData;
    public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        jumpData = airborneData.JumpData;
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    private void Jump()
    {
        Vector3 jumpForce = Vector3.up;

        if (stateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            jumpForce.Scale(jumpData.IdleJumpForce);
        }
        else
        {
            jumpForce += stateMachine.PlayerMovement.transform.forward * stateMachine.ReusableData.MovementInput.y;
            jumpForce += stateMachine.PlayerMovement.transform.right * stateMachine.ReusableData.MovementInput.x;

            jumpForce.Scale(jumpData.MoveJumpForce);
        }

        stateMachine.PlayerMovement.Rigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }
}
