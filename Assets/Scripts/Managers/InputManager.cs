using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager
{
   /* public static GameInputAction inputActions;
    public static GameInputAction.PlayerActions playerActions;
    public static GameInputAction.UIActions uiActions;
*/
    //All Player Action
    public static Action<Vector2> movementAction;
    public static Action attackStartedAction;
    public static Action attackCanceledAction;
    //All UI Actions

    public static void Initialize()
    {
       /* inputActions = new GameInputAction();
        playerActions = inputActions.Player;
        uiActions = inputActions.UI;*/

        InitealizeCallback();
    }

    private static void InitealizeCallback()
    {
        //Player actions
        /*playerActions.Fire.started += FireStarted;
        playerActions.Fire.canceled += FileCanceled;*/
        //UI Actions

    }

    public static void Update()
    {
        //movementAction?.Invoke(playerActions.Move.ReadValue<Vector2>());
    }

    #region Enable and Disable InputActions
    public static void EnablePlayerActions()
    {
        /*playerActions.Enable();
        uiActions.Disable();*/
    }

    public static void EnableUIActions()
    {/*
        playerActions.Disable();
        uiActions.Enable();*/
    }

    public static void EnableBothActoins()
    {
        /*playerActions.Enable();
        uiActions.Enable();*/
    }

    public static void DisableBothActoins()
    {
        /*playerActions.Disable();
        uiActions.Disable();*/
    }
    #endregion

    #region Callback Methods
    private static void FireStarted(InputAction.CallbackContext ctx)
    {
        attackStartedAction?.Invoke();
    }

    private static void FileCanceled(InputAction.CallbackContext ctx)
    {
        attackCanceledAction?.Invoke();
    }
    #endregion
}
