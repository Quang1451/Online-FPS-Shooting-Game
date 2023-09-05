using Cinemachine;
using Sirenix.OdinInspector;
using System;
using UnityEngine;


public class MVCPlayerView : BaseView
{
    public Transform cameraLookAt;
    public PlayerCapsuleColliderUtility colliderUtility;
    public CharacterLookRotation lookRotation;

    public Rigidbody Rigidbody { get; private set;}

    private void OnValidate()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();
    }

    public override void Initialize()
    {
        colliderUtility.Initialize(gameObject);
        colliderUtility.CaculateCapsuleColliderDimesions();

        Rigidbody = GetComponent<Rigidbody>();

        lookRotation.SetInputProvider(GetComponent<CinemachineInputProvider>());
        lookRotation.HideCursor();
    }

    public override void SpawnModel(Action action)
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

    public void Rotate(float SpeedRoation)
    {
        lookRotation.horizontalAxis.Update(Time.deltaTime);
        lookRotation.verticalAxis.Update(Time.deltaTime);
                
        cameraLookAt.eulerAngles = new Vector3(lookRotation.verticalAxis.Value, lookRotation.horizontalAxis.Value, 0f);

        float yawCamera = GameManager.Instance.mainCamera.transform.rotation.eulerAngles.y;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f), SpeedRoation * Time.deltaTime);
    }

}

[Serializable]
public class CharacterLookRotation
{
    public AxisState horizontalAxis;
    public AxisState verticalAxis;

    public void SetInputProvider(CinemachineInputProvider inputProvider)
    {
        horizontalAxis.SetInputAxisProvider(0, inputProvider);
        verticalAxis.SetInputAxisProvider(1, inputProvider);
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}