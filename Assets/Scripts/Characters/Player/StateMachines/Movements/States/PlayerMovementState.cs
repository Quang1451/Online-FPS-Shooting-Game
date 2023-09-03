using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        stateMachine = playerMovementStateMachine;
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
    }

    public virtual void OnTriggerExit(Collider collider)
    {
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
        stateMachine.PlayerController.view.Rigidbody.AddForce(movementDirection * movementSpeed - currentHorizontalVelocity, ForceMode.VelocityChange);

        stateMachine.PlayerController.animation.UpdateMoveDirection(stateMachine.ReusableData.MovementInput);
    }    

    protected virtual void AddInputActionsCallbacks()
    {
    }

    protected virtual void RemoveInputAcionsCallbacks()
    {
    }

    private float GetMovementSpeed()
    {
        return stateMachine.ReusableData.MovementSpeedModifier;
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 horizontal = stateMachine.ReusableData.MovementInput.x * stateMachine.PlayerController.view.transform.right;
        Vector3 vertical = stateMachine.ReusableData.MovementInput.y * stateMachine.PlayerController.view.transform.forward;
        return horizontal + vertical;
    }

    protected Vector3 GetPlayerHorizontalVelocity()
    {
        Vector3 horizontalVelocity = stateMachine.PlayerController.view.Rigidbody.velocity;
        horizontalVelocity.y = 0;
        return horizontalVelocity;
    }
    #endregion
}
