using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharaterType
{
    Player,
    Enemy,
    Pet,
}

public class ChareterManager : MonoBehaviour
{
    public MVCPlayerView playerView;

    List<MVCController> listCharacter = new List<MVCController>();

    public void Initialize()
    {
        listCharacter = new List<MVCController>();
    }

    public void CreateCharacter(CharaterType type)
    {
        //create model
        var model = MVCFactory.CreateModel(type);
        
        //create view
        var view = MVCFactory.CreateView(type);

        //create controller
        var controller = MVCFactory.CreateController(type);
        controller.SetModel(model);
        controller.SetView(view);
        controller.Initialize();
        listCharacter.Add(controller);
    }

    public void Update()
    {
        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].Update();
        }
    }
    
    public void LateUpdate()
    {
        for (int i = 0; i < listCharacter.Count; i++)
        {
            listCharacter[i].LateUpdate();
        }
    }
}


public static class MVCFactory
{
    public static MVCIData CreateModel(CharaterType type)
    {
        //var data = new BaseData();
        switch (type)
        {
            case CharaterType.Player:
               var data2 = new MVCPlayerModel();
                //apply data tu design / server....
               data2.atk = 1;

               return data2;
                break;
            case CharaterType.Enemy:
                return new MVCEnemyModel();
                break;
            case CharaterType.Pet:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return null;
    }

    public static MVCView CreateView(CharaterType type)
    {
        switch (type)
        {
            case CharaterType.Player:
            
                break;
            case CharaterType.Enemy:
              
                break;
            case CharaterType.Pet:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return null;
    }

    public static MVCController CreateController(CharaterType type)
    {
        switch (type)
        {
            case CharaterType.Player:
            
                break;
            case CharaterType.Enemy:
              
                break;
            case CharaterType.Pet:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return null;
    }
}