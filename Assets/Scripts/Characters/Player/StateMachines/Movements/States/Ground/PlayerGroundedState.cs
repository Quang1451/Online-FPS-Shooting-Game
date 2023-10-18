using System;
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

    public override void Update()
    {
        base.Update();
        stateMachine.View.animationUtility.Animator.SetFloat(stateMachine.View.animationUtility.MoveX, stateMachine.ReusableData.MovementInput.x, 0.1f, Time.deltaTime);
        stateMachine.View.animationUtility.Animator.SetFloat(stateMachine.View.animationUtility.MoveY, stateMachine.ReusableData.MovementInput.y, 0.1f, Time.deltaTime);
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

            float distanceToFloatingPoint = stateMachine.PlayerMovement.View.colliderUtility.CapsuleColliderData.ColliderCenterInLocalSpace.y * stateMachine.PlayerMovement.transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0f) return;

            float amountToLift = distanceToFloatingPoint * slopeData.StepReachForce - GetPlayerVerticalVelocity().y;

            Vector3 liftForce = new Vector3(0f, amountToLift, 0f);

            stateMachine.PlayerMovement.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }
    #endregion

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();
        InputManager.playerActions.Crouch.started += OnCrouchStarted;
        InputManager.playerActions.Crouch.canceled += OnCrouchCancled;
    }

    protected override void RemoveInputAcionsCallbacks()
    {
        base.RemoveInputAcionsCallbacks();
    }

    #region Callback Methods
  
    protected virtual void OnCrouchStarted(InputAction.CallbackContext obj)
    {
        stateMachine.ReusableData.IsCrouching = true;
    }

    protected virtual void OnCrouchCancled(InputAction.CallbackContext obj)
    {
        stateMachine.ReusableData.IsCrouching = false;
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
