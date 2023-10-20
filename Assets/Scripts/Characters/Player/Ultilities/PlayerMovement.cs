using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MVCPlayerView))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementSO MovementSO { get; private set; }
    public MVCPlayerView View { get; private set; }
    public PlayerStateReusubleData ReusubleData { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    public void Initialize(IData data = null)
    {
        ReusubleData = ((MovementData)data).reusubleData;

        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.StandIdlingState);
    }

    private void Awake()
    {
        View = GetComponent<MVCPlayerView>();
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    public void DoUpdate()
    {
        movementStateMachine?.Update();
        movementStateMachine?.HandleInput();
    }

    public void DoPhysicUpdate()
    {
        movementStateMachine?.PhysicsUpdate();
    }

    public void OnAnimationExitEvent()
    {
        movementStateMachine?.OnAnimationExitEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        movementStateMachine?.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        movementStateMachine?.OnTriggerExit(other);
    }

    private void OnEnable()
    {
        View.ActiveAiming += SetReusubleAimingData;
    }

    private void OnDisable()
    {
        View.ActiveAiming -= SetReusubleAimingData;
    }

    private void SetReusubleAimingData(bool value)
    {
        ReusubleData.IsAiming = value;
        View.animationUtility.Animator.SetBool(View.animationUtility.IsAiming, value);
    }
}

public class MovementData: IData
{
    public PlayerStateReusubleData reusubleData;
}