using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    private SlopeData slopeData;
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
        slopeData = stateMachine.PlayerMovement.View.colliderUtility.SlopeData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.ReusableData.JumpDelayTime = Time.time + groundedData.GroundToJumpDelayTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Float();
    }

    #region Main Methods
    private void Float()
    {
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.PlayerMovement.View.colliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.FloatRayDistance, stateMachine.PlayerMovement.MovementSO.GroundLayer, QueryTriggerInteraction.Ignore))
        {

            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            /*float slopeSpeedModifier = SetSlopeSpeedModifiedOnAngle(groundAngle);

            if (slopeSpeedModifier == 0f) return;*/

            float distanceToFloatingPoint = stateMachine.PlayerMovement.View.colliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.PlayerMovement.transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0f) return;

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

            stateMachine.PlayerMovement.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }

    /*private float SetSlopeSpeedModifiedOnAngle(float angle)
    {
        float slopeSpeedModifier = groundedData.SlopeSpeedAngles.Evaluate(angle);

        if (stateMachine.ReusableData.MovementOnSlopesSpeedModifier != slopeSpeedModifier)
        {
            stateMachine.ReusableData.MovementOnSlopesSpeedModifier = slopeSpeedModifier;
        }

        return slopeSpeedModifier;
    }*/
    #endregion

    #region Callback Methods
    protected void OnCrouchChange(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Started:
                stateMachine.ChangeState(stateMachine.CrouchingState);
                break;
            case InputActionPhase.Canceled:
                stateMachine.ChangeState(stateMachine.StandingState);
                break;
        }
    }

    protected override void OnContactWithGroundExit(Collider collider)
    {
        base.OnContactWithGroundExit(collider);

        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.PlayerMovement.View.colliderUtility.CapsuleColliderData.Collider.bounds.center;

        Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.PlayerMovement.View.colliderUtility.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);

        if (!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, groundedData.GroundToFallRayDistance, stateMachine.PlayerMovement.MovementSO.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            OnFall();
        }
    }

    protected virtual void OnFall()
    {
        stateMachine.ChangeState(stateMachine.FallingState);
    }
    #endregion
}
