using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class RotateStatus : MonoBehaviour
{
    [SerializeField] private AxisState HorizontalAxis;
    [SerializeField] private AxisState VerticalAxis;

    private CinemachineInputProvider _inputProvider;
    private ParentConstraint _parentConstraint;
    private ConstraintSource _constraintSource;
    private void Awake()
    {
        _inputProvider = GetComponent<CinemachineInputProvider>();
        _parentConstraint = GetComponent<ParentConstraint>();

        HorizontalAxis.SetInputAxisProvider(0, _inputProvider);
        VerticalAxis.SetInputAxisProvider(1, _inputProvider);
    }

    public void DoFixedUpdate()
    {
        HorizontalAxis.Update(Time.fixedDeltaTime);
        VerticalAxis.Update(Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(VerticalAxis.Value, HorizontalAxis.Value, 0f);
    }

    public void SetConstraint(Transform transform)
    {
        _constraintSource.sourceTransform = transform;
        _constraintSource.weight = 1;
        _parentConstraint.AddSource(_constraintSource);
        _parentConstraint.enabled = true;
    }
}
