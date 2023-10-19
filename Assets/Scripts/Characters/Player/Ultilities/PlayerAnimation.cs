using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[Serializable]
public class PlayerAnimation
{
    public Animator Animator;
    public RigBuilder RigBuilder;

    [Header("Animation name:")]
    public string Idle = "Idle";
    public string Run = "Run";
    public string Step = "Step";

    public string Jump = "Jump";
    public string Fall = "Fall";

    public string IsAiming = "IsAiming";

    [Header("Layers:")]
    public int CrouchLayer = 1;

    [Header("Paramaters:")]
    public string MoveX = "MoveX";
    public string MoveY = "MoveY";

    [Header("Rig Layers:")]
    public Rig RigWeaponAiming;
    public Rig RigWeaponHolding;
    public Rig RigHands;

    [Header("IK Constraints:")]
    public TwoBoneIKConstraint LeftHandIk;
    public TwoBoneIKConstraint RightHandIk;

    [Header("Multi Aim Constraint")]
    public List<MultiAimConstraint> AimConstraintList;

    public void Initialize()
    {
        RigWeaponAmingChange(false);
        SetArming();
    }


    public Tween SmoothDampAnimatorLayer(int layer, float start, float end, float duration = 0.1f)
    {
        return DOTween.To(() => start, x => start = x, end, duration)
           .SetEase(Ease.Linear).OnUpdate(() =>
           {
               Animator.SetLayerWeight(layer, start);
           });
    }

    public void RigWeaponAmingChange(bool IsAiming)
    {
        RigWeaponAiming.weight = IsAiming ? 1 : 0;
        RigWeaponHolding.weight = IsAiming ? 0 : 1;
    }

    public void SetArming()
    {
        foreach (MultiAimConstraint aimConstraint in AimConstraintList)
        {
            var data = aimConstraint.data.sourceObjects;
            data.SetTransform(0, GameManager.Instance.GetAmingTransform());
            aimConstraint.data.sourceObjects = data;
        }
        RigBuilder.Build();
    }
}
