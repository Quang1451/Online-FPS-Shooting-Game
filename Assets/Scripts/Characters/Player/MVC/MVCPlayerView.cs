using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class MVCPlayerView : BaseView
{
    public Transform cameraLookAt;
    public PlayerAnimation playerAnimation;
    public PlayerController playerController;
    public PlayerCapsuleColliderUtility colliderUtility;
    
    private void OnValidate()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();
    }

    public override void Initialize()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();

        SetCamera();
        playerAnimation.SetArm();
    }

    private void SetCamera()
    {
        GameManager.Instance.cinemachineFreeLook.Follow = transform;
        GameManager.Instance.cinemachineFreeLook.LookAt = cameraLookAt;
    }
    public override void SpawnModel(System.Action action)
    {
        action?.Invoke();
    }

    public void ChangCapsuleColliderData(ColliderDataType type)
    {
        colliderUtility.SwithColiiderData(type);
    }

    [Button]
    public void Stand()
    {
        ChangCapsuleColliderData(ColliderDataType.Default);
    }

    [Button]
    public void Crouch()
    {
        ChangCapsuleColliderData(ColliderDataType.Crouch);
    }

}
