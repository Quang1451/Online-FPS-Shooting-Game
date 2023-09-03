using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager
{
    public static FPSInputActions inputAction;
    public static FPSInputActions.PlayerActions playerActions;
    public static FPSInputActions.UIActions uiActions;

    public static void Initialize()
    {
        if(inputAction == null)
        {
            inputAction = new FPSInputActions();
            playerActions = inputAction.Player;
            uiActions = inputAction.UI;
        }   
    }

    #region Enable and Disable InputActions
    public static void EnablePlayerActions()
    {
        playerActions.Enable();
        uiActions.Disable();
    }

    public static void EnableUIActions()
    {
        playerActions.Disable();
        uiActions.Enable();
    }

    public static void EnableBothActoins()
    {
        playerActions.Enable();
        uiActions.Enable();
    }

    public static void DisableBothActoins()
    {
        playerActions.Disable();
        uiActions.Disable();
    }
    #endregion
}
