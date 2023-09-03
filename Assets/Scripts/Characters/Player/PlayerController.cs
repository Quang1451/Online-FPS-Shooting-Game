using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IController
{
    public PlayerAnimation playerAnimation;
    public PlayerStateReusubleData reusubleData;
    public PlayerMovementStateMachine movementStateMachine;

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        reusubleData = new PlayerStateReusubleData();
        movementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.StandingState);
    }

    public void DoUpdate()
    {
        movementStateMachine.Update();
        movementStateMachine.HandleInput();
    }

    public void DoFixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }
}
