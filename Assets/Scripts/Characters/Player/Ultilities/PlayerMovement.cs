using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MVCPlayerView))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementSO MovementSO { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public MVCPlayerView View { get; private set; }
    public MovementData Data { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        View = GetComponent<MVCPlayerView>();
    }
    
    public void Initialize(IData data = null)
    {
        Data = (MovementData) data;
        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.StandIdlingState);
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
    }

    private void OnDisable()
    {   
    }
}

public class MovementData: IData
{
    public PlayerStateReusubleData reusubleData;
}