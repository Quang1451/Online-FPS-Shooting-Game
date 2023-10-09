using Cinemachine;
using Sirenix.OdinInspector;
using System;
using UnityEngine;


public class MVCPlayerView : BaseView
{
    [Header("Collider Settings:")]
    [SerializeField] private Transform cameraLookAt;
    public PlayerCapsuleColliderUtility colliderUtility;
    [Header("Animation Settings:")]
    [SerializeField] private Animator Animator;
    public string Stand = "Stand";
    public string Crouch = "Crouch";

    public string Jump = "Jump";
    public string Fall = "Fall";

    [Header("Paramaters:")]
    public string MoveX = "MoveX";
    public string MoveY = "MoveY";

    public Transform MainCameraTransform { get; private set; }
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        MainCameraTransform = Camera.main.gameObject.transform;
        GameManager.Instance.SetVirtualCamera(transform, cameraLookAt);
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

    public void CrossFadeAnimation(string animationName, float duration)
    {

    }

    public void UpdateMoveDirection(Vector2 direction, float dampTime = 0.1f)
    {
        Animator.SetFloat(MoveX, direction.x, dampTime, Time.deltaTime);
        Animator.SetFloat(MoveY, direction.y, dampTime, Time.deltaTime);
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
}
