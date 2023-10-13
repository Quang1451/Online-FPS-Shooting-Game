using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateReusubleData
{
    public Vector2 MovementInput;
    public Vector2 LookInput;
    public float MovementSpeedModifier = 1f;
    public float JumpDelayTime;
    
    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;

    public ref Vector3 CurrentTargetRotation
    {
        get
        {
            return ref currentTargetRotation;
        }
    }

    public ref Vector3 TimeToReachTargetRotation
    {
        get
        {
            return ref timeToReachTargetRotation;
        }
    }

    public ref Vector3 DampedTargetRotationCurrentVelocity
    {
        get
        {
            return ref dampedTargetRotationCurrentVelocity;
        }
    }

    public ref Vector3 DampedTargetRotationPassedTime
    {
        get
        {
            return ref dampedTargetRotationPassedTime;
        }
    }

    public PlayerRotationData RotationData { get; set; }

    public bool IsCrouching;
    public bool IsAiming;
}