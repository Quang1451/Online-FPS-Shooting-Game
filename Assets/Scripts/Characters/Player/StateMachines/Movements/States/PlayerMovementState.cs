using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;
    protected PlayerGroundedData groundedData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
        groundedData = stateMachine.PlayerMovement.MovementSO.GroundedData;
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
    private void ReadMovementInput()
    {
        stateMachine.ReusableData.MovementInput = InputManager.playerActions.Move.ReadValue<Vector2>();
    }

    protected virtual void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        float movementSpeed = GetMovementSpeed();
        Vector3 currentHorizontalVelocity = GetPlayerHorizontalVelocity();
        stateMachine.PlayerMovement.Rigidbody.AddForce(movementDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);

        stateMachine.PlayerMovement.PlayerAnimation.UpdateMoveDirection(stateMachine.ReusableData.MovementInput);
    }    

    protected virtual void AddInputActionsCallbacks()
    {
    }

    protected virtual void RemoveInputAcionsCallbacks()
    {
    }

    private float GetMovementSpeed()
    {
        return stateMachine.ReusableData.MovementSpeedModifier * groundedData.BaseSpeed;
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 horizontal = stateMachine.ReusableData.MovementInput.x * stateMachine.PlayerMovement.transform.right;
        Vector3 vertical = stateMachine.ReusableData.MovementInput.y * stateMachine.PlayerMovement.transform.forward;
        return horizontal + vertical;
    }

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 horizontalVelocity = stateMachine.PlayerMovement.Rigidbody.velocity;
        horizontalVelocity.y = 0;
        return horizontalVelocity;
    }

    protected Vector3 GetPlayerVerticalVelocity()
    {
        Vector3 playerVerticalVelocity = stateMachine.PlayerMovement.Rigidbody.velocity;
        playerVerticalVelocity.x = 0f;
        playerVerticalVelocity.z = 0f;
        return playerVerticalVelocity;
    }
    #endregion

    #region Callback Methods
    protected virtual void OnContactWithGroundEnter(Collider collider)
    {
    }

    protected virtual void OnContactWithGroundExit(Collider collider)
    {
    }
    #endregion
}
