using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected PlayerGroundedData groundedData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
        groundedData = stateMachine.PlayerMovement.MovementSO.GroundedData;

        InitializeData();
    }

    #region State Methods
    public virtual void Enter()
    {
        Debug.Log("Movement State: " + GetType().Name);
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputAcionsCallbacks();
    }


    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void Update()
    {
    }

    public virtual void OnAnimationCanTransitionAttack()
    {
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnAnimationTransitionEvent()
    {
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (stateMachine.PlayerMovement.MovementSO.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGroundEnter(collider);
            return;
        }
    }

    public virtual void OnTriggerExit(Collider collider)
    {
        if (stateMachine.PlayerMovement.MovementSO.IsGroundLayer(collider.gameObject.layer))
        {
            OnContactWithGroundExit(collider);
            return;
        }
    }

    #endregion

    #region Main Methods
    private void InitializeData()
    {
        SetBaseRotationData();
    }

    protected void SetBaseRotationData()
    {
        stateMachine.ReusableData.RotationData = groundedData.BaseRotationData;
        stateMachine.ReusableData.TimeToReachTargetRotation = groundedData.BaseRotationData.TargetRotationReachTime;
    }

    protected virtual void Move()
    {
        if (stateMachine.ReusableData.MovementInput == Vector2.zero || stateMachine.ReusableData.MovementSpeedModifier == 0f) return;

        Vector3 movementDirection = GetMovementInputDirection();

        float targetRotationYAngle = Rotate(movementDirection);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

        stateMachine.View.Rigidbody.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }

    protected Vector3 GetPlayerVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = stateMachine.View.Rigidbody.velocity;
        playerVerticalVelocity.x = 0f;
        playerVerticalVelocity.z = 0f;
        return playerVerticalVelocity;
    }

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 playerHorizontalVelocity = stateMachine.View.Rigidbody.velocity;
        playerHorizontalVelocity.y = 0f;
        return playerHorizontalVelocity;
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);
        return directionAngle;
    }
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.ReusableData.MovementInput.x, 0f, stateMachine.ReusableData.MovementInput.y);
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shoulConsiderCameraRoation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shoulConsiderCameraRoation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != stateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        RotateTowardsTargetRotation();
        return directionAngle;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.View.transform.eulerAngles.y;

        if (currentYAngle == stateMachine.ReusableData.CurrentTargetRotation.y) return;

        float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableData.CurrentTargetRotation.y, ref stateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableData.TimeToReachTargetRotation.y - stateMachine.ReusableData.DampedTargetRotationPassedTime.y);

        stateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0.0f, smoothedYAngle, 0.0f);
        stateMachine.View.Rigidbody.MoveRotation(targetRotation);
    }

    protected float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.View.MainCameraTransform.eulerAngles.y;

        if (angle > 360f) angle -= 360f;
        return angle;
    }

    protected float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f) directionAngle += 360f;
        return directionAngle;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = groundedData.BaseSpeed * stateMachine.ReusableData.MovementSpeedModifier;
        return movementSpeed;
    }

    protected virtual void AddInputActionsCallbacks()
    {
        InputManager.playerActions.Move.performed += OnMovementPerformed;
        InputManager.playerActions.Move.canceled += OnMovementCanceled;
    }

    protected virtual void RemoveInputAcionsCallbacks()
    {
        InputManager.playerActions.Move.performed -= OnMovementPerformed;
        InputManager.playerActions.Move.canceled -= OnMovementCanceled;
    }

    protected void ResetVelocity()
    {
        stateMachine.View.Rigidbody.velocity = Vector3.zero;
    }

    protected void ResetVelocityHorizontally()
    {
        stateMachine.View.Rigidbody.velocity = new Vector3(0f, stateMachine.View.Rigidbody.velocity.y, 0f);
    }

    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
        Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();
        Vector2 playerHorizontalMovement = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);

        return playerHorizontalMovement.magnitude > minimumMagnitude;
    }

    #endregion

    #region Reusable Methods
    private void ReadMovementInput()
    {
        stateMachine.ReusableData.MovementInput = InputManager.playerActions.Move.ReadValue<Vector2>();
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
        stateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }

    #endregion

    #region Callback Methods
    protected virtual void OnContactWithGroundEnter(Collider collider)
    {
    }

    protected virtual void OnContactWithGroundExit(Collider collider)
    {
    }

    protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
    {
    }
    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
    }
    #endregion
}
