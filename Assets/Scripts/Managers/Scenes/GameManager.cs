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
    Enemy
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private AssetReference character;
    public Camera mainCamera;
    public Transform aimingPos;

    private List<MVCIController> _listCharacter;

    public void Initialize()
    {
        _listCharacter = new List<MVCIController>();

        InputManager.Initialize();
        InputManager.EnableBothActoins();
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
        MVCFactory.CreateView(type, OnCreatedCharacterView, character);
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

    [Button]
    private void CreatePlayer()
    {
        CreateCharacter(CharacterType.Player);
    }
}

public static class MVCFactory
{
    public static void CreateView(CharacterType type, Action<MVCIView, CharacterType> callback, AssetReference reference)
    {   
        reference.InstantiateAsync().Completed += view =>
        {
            switch (type)
            {
                case CharacterType.Player:
                    callback?.Invoke(view.Result.GetComponent<MVCPlayerView>(), type);
                    return;
                case CharacterType.Enemy:
                    
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
            case CharacterType.Enemy:
                break;
        }
        return null;
    }

    public static MVCIController CreateController(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Player:
                return new MVCPlayerController();
            case CharacterType.Enemy:
                break;
        }
        return null;
    }
}
