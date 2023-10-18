using Cinemachine;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum CharacterType
{
    Player,
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private AssetReference characterAsset;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [SerializeField] private CinemachineVirtualCamera VirtualCameraAim;

    [SerializeField] private Transform AimingTransform;

    private List<MVCIController> _listCharacter;

    public void Initialize()
    {
        InputManager.Initialize();
        InputManager.EnableBothActoins();
        _listCharacter = new List<MVCIController>();

        DisableAimCamera();
    }

    #region Unity
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        foreach(var character in _listCharacter)
        {
            character.Update();
        }
    }

    private void FixedUpdate()
    {
        foreach (var character in _listCharacter)
        {
            character.FixedUpdate();
        }
    }

    private void LateUpdate()
    {
        foreach (var character in _listCharacter)
        {
            character.LateUpdate();
        }
    }
    #endregion

    #region Create Charater
    public void CreateCharacter(CharacterType type)
    {
        MVCFactory.CreateView(type, characterAsset, OnCreatedCharacterView);
    }

    public void OnCreatedCharacterView(MVCIView view, CharacterType type)
    {
        var model = MVCFactory.CreateModel(type);
        var controller = MVCFactory.CreateController(type);
  
        controller.SetModel(model);
        controller.SetView(view);

        controller.Initialize();
        _listCharacter.Add(controller);
    }
    #endregion


    public CinemachinePOV GetCinemachinePOV()
    {
        return VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void EnableAimCamera()
    {
        VirtualCameraAim.enabled = true;
    }

    public void DisableAimCamera()
    {
        VirtualCameraAim.enabled = false;
    }

    //Test
    [Button]
    private void CreatePlayer()
    {
        CreateCharacter(CharacterType.Player);
    }

    public void SetVirtualCamera(Transform follow, Transform lookAt)
    {
        VirtualCamera.Follow = follow;
        VirtualCamera.LookAt = lookAt;

        VirtualCameraAim.Follow = follow;
        VirtualCameraAim.LookAt = lookAt;
    }

    public Transform GetAmingTransform()
    {
        return AimingTransform;
    }
}

public static class MVCFactory
{
    public static void CreateView(CharacterType type, AssetReference reference, Action<MVCIView,CharacterType> callback)
    {
        reference.InstantiateAsync().Completed += view =>
        {
            switch (type)
            {
                case CharacterType.Player:
                    callback?.Invoke(view.Result.GetComponent<MVCPlayerView>(), type);
                    return;
            }
        };
    }

    public static MVCIData CreateModel(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                return new MVCPlayerModel();
        }
        return null;
    }

    public static MVCIController CreateController(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                return new MVCPlayerController();
        }
        return null;
    }
}
