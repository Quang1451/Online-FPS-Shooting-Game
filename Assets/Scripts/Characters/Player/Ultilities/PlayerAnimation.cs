using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : AnimationController
{
    [SerializeField] private RigBuilder _rigBuilder;

    public string MoveX = "MoveX";
    public string MoveY = "MoveY";
    public string IsCrouch = "IsCrouch";
    public void UpdateMoveDirection(Vector2 direction, float dampTime = 0.1f)
    {
        animator.SetFloat(MoveX, direction.x, dampTime, Time.deltaTime);
        animator.SetFloat(MoveY, direction.y, dampTime, Time.deltaTime);
    }

    public void SetCrouch(bool value = false)
    {
        animator.SetBool(IsCrouch, value);
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
