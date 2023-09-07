using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerAnimation), typeof(MVCPlayerView))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementSO MovementSO { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public PlayerAnimation PlayerAnimation { get; private set; }
    public MVCPlayerView View { get; private set; }
    public PlayerMovementData Data { get; private set; }

    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        View = GetComponent<MVCPlayerView>();
    }
    
    public void Initalize(IData data = null)
    {
        Data = (PlayerMovementData) data;

        movementStateMachine = new PlayerMovementStateMachine(this);
        movementStateMachine.ChangeState(movementStateMachine.StandingState);
    }

    public void DoUpdate()
    {
        movementStateMachine?.Update();
        movementStateMachine?.HandleInput();

        View.Rotate(MovementSO.SpeedRotation);
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
}

public class PlayerMovementData: IData
{
    public PlayerStateReusubleData reusubleData;
}