using UnityEngine;
using System;

[Serializable]
public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
{
    [field: SerializeField] public DefaultColliderData CrouchColliderData { get; private set; }

    public void SwithColiiderData(ColliderDataType type)
    {
        switch (type)
        {
            case ColliderDataType.Default:
                TempColliderData = DefaultColliderData;
                break;
            case ColliderDataType.Crouch:
                TempColliderData = CrouchColliderData;
                break;
        }
        CaculateCapsuleColliderDimesions();
    }
}

public enum ColliderDataType
{
    Default,
    Crouch
}