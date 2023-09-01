using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MVCPlayerView : BaseView
{
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
