using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;


public class MVCPlayerView : BaseView
{
    [Header("Collider Settings:")]
    [SerializeField] private Transform cameraLookAt;
    public PlayerCapsuleColliderUtility colliderUtility;
    [Header("Animation Settings:")]
    public Animator Animator;
    public string Idle = "Idle";
    public string Run = "Run";
    public string Step = "Step";

    public string Jump = "Jump";
    public string Fall = "Fall";

    [Header("Layers:")]
    public int CrouchLayer = 1;
    [Header("Paramaters:")]
    public string MoveX = "MoveX";
    public string MoveY = "MoveY";

    public Transform MainCameraTransform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        MainCameraTransform = Camera.main.gameObject.transform;
        GameManager.Instance.SetVirtualCamera(cameraLookAt, cameraLookAt);
    }

    public override void Initialize()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();
        Rigidbody = GetComponent<Rigidbody>();
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

    public void SetArm()
    {
        /*foreach (MultiAimConstraint aimConstraint in GetComponentsInChildren<MultiAimConstraint>())
        {
            var data = aimConstraint.data.sourceObjects;
            data.SetTransform(0, GameManager.Instance.aimingPos);
            aimConstraint.data.sourceObjects = data;
        }

        _rigBuilder.Build();*/
    }

    public Tween SmoothDampAnimatorLayer(int layer, float start, float end, float duration = 0.1f)
    {
        return DOTween.To(() => start, x => start = x, end, duration)
           .SetEase(Ease.Linear).OnUpdate(() =>
           {
               Animator.SetLayerWeight(layer, start);
           });
    }
}
