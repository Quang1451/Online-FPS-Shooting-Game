using Cinemachine;
using System;
using UnityEngine;


public class MVCPlayerView : BaseView
{
    public Transform cameraTarget;
    [Header("Collider Settings:")]
    public PlayerCapsuleColliderUtility colliderUtility;
    [Header("Animation Settings:")]
    public PlayerAnimation animationUtility;

    public Transform MainCameraTransform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        MainCameraTransform = Camera.main.gameObject.transform;
        Rigidbody = GetComponent<Rigidbody>();
    }

    public override void Initialize()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();
        animationUtility.Initialize();

        GameManager.Instance.SetCamera(cameraTarget);
    }

    private void OnValidate()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();
    }

    public override void SpawnModel(Action action)
    {
        action?.Invoke();
    }

    public void ChangCapsuleColliderData(ColliderDataType type)
    {
        colliderUtility.SwithColiiderData(type);
    }

    public void Standing()
    {
        ChangCapsuleColliderData(ColliderDataType.Default);
    }

    public void Crouching()
    {
        ChangCapsuleColliderData(ColliderDataType.Crouch);
    }
}