using System;
using UnityEngine;

public class CapsuleColliderUtility
{
    public CapsuleColliderData CapsuleColliderData { get; private set; }
    [field: SerializeField] public DefaultColliderData DefaultColliderData { get; private set; }
    [field: SerializeField] public SlopeData SlopeData { get; private set; }

    protected DefaultColliderData TempColliderData;

    public void Initialize(GameObject gameObject)
    {
        if (CapsuleColliderData != null) return;

        CapsuleColliderData = new CapsuleColliderData();
        CapsuleColliderData.Initialize(gameObject);

        OnInitialize();
    }

    protected virtual void OnInitialize()
    {
        TempColliderData = DefaultColliderData;
    }

    public void CaculateCapsuleColliderDimesions()
    {
        SetCapsuleColliderRadius(TempColliderData.Radius);
        SetCapsuleColliderHeight(TempColliderData.Height * (1f - SlopeData.StepHeightPercentage));

        RecalculateCapsuleColliderCenter();

        if (CapsuleColliderData.Collider.height / 2f < CapsuleColliderData.Collider.radius)
        {
            SetCapsuleColliderRadius(CapsuleColliderData.Collider.height / 2f);
        }

        CapsuleColliderData.UpdateColliderData();
    }

    public void SetCapsuleColliderRadius(float radius)
    {
        CapsuleColliderData.Collider.radius = radius;
    }

    public void SetCapsuleColliderHeight(float height)
    {
        CapsuleColliderData.Collider.height = height;
    }

    public void RecalculateCapsuleColliderCenter()
    {
        float colliderHeightDifferent = TempColliderData.Height - CapsuleColliderData.Collider.height;

        Vector3 newColliderCenter = new Vector3(0f, TempColliderData.CenterY + (colliderHeightDifferent / 2f), 0f);

        CapsuleColliderData.Collider.center = newColliderCenter;
    }
}
