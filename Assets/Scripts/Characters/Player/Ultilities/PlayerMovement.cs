using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MVCPlayerView))]
public class PlayerMovement : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementSO MovementSO { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public MVCPlayerView View { get; private set; }
    public MovementData Data { get; private set; }

    public Action AimChangeAction;
    
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

        AimChangeAction = OnAimChange;

        InputManager.playerActions.Fire.started += TEst;
    }

    private void TEst(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        AimChangeAction?.Invoke();
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
    
    private void OnAimChange()
    {
        Data.reusubleData.IsAiming = !Data.reusubleData.IsAiming;
        View.animationUtility.Animator.SetBool(View.animationUtility.IsAiming, Data.reusubleData.IsAiming);
        View.animationUtility.RigWeaponAmingChange(Data.reusubleData.IsAiming);
        
        if(Data.reusubleData.IsAiming)
        {
            GameManager.Instance.EnableAimCamera();
            
            Data.reusubleData.CurrentTargetRotation = new Vector3 (Mathf.DeltaAngle(0,View.MainCameraTransform.eulerAngles.x), View.MainCameraTransform.eulerAngles.y, 0f);
            Data.reusubleData.DampedTargetRotationPassedTime = Vector3.zero;
        }
        else
        {
            GameManager.Instance.DisableAimCamera();

            /*View.CinemachinePOV.m_HorizontalAxis.Value = Data.reusubleData.CurrentTargetRotation.y;
            View.CinemachinePOV.m_VerticalAxis.Value = Mathf.DeltaAngle(0,Data.reusubleData.CurrentTargetRotation.x);*/
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {   
    }
}

public class MovementData: IData
{
    public PlayerStateReusubleData reusubleData;
}