using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : AnimationController
{
    [SerializeField] private RigBuilder _rigBuilder;

    public string MoveX = "MoveX";
    public string MoveY = "MoveY";

    public void UpdateMoveDirection(Vector2 direction)
    {
        animator.SetFloat(MoveX, direction.x);
        animator.SetFloat(MoveY, direction.y);
    }

    public void SetArm()
    {
        foreach (MultiAimConstraint aimConstraint in GetComponentsInChildren<MultiAimConstraint>())
        {
            var data = aimConstraint.data.sourceObjects;
            data.SetTransform(0, GameManager.Instance.aimingPos);
            aimConstraint.data.sourceObjects = data;
        }

        _rigBuilder.Build();
    }
}
