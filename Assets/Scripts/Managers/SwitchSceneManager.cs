using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SwitchSceneManager
{
    //Scene key
    public static string MAINMENU = "Assets/Scenes/MainMenu.unity";
    public static string GAMEPLAY = "Assets/Scenes/Gameplay.unity";
    
  
    private static SceneInstance _previousMap;

    public static IEnumerator<float> LoadSceneAsync(string key, LoadSceneMode loadSceneMode, LoadingUI ui,
        string subSceneKey = null)
    {
        AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(key, loadSceneMode);
        if (loadSceneMode == LoadSceneMode.Additive)
        {
            ClearPreviousMap();

            sceneHandle.Completed += (handle) =>
            {
                _previousMap = handle.Result;
            };
        }


        //Clear previous sub scene
        if (subSceneKey != null && loadSceneMode == LoadSceneMode.Single)
        {
            AsyncOperationHandle<SceneInstance> subSceneHandle = Addressables.LoadSceneAsync(subSceneKey, LoadSceneMode.Additive);

            subSceneHandle.Completed += (handle) =>
            {
                _previousMap = handle.Result;
            };

            while (sceneHandle.Status != AsyncOperationStatus.Succeeded &&
                   subSceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                ui.SetPercent((sceneHandle.PercentComplete + subSceneHandle.PercentComplete) / (0.99f * 2));
                yield return Timing.WaitForOneFrame;
            }
        }
        else
        {
            while (sceneHandle.Status != AsyncOperationStatus.Succeeded)
            {
                ui.SetPercent(sceneHandle.PercentComplete);
                yield return Timing.WaitForOneFrame;
            }
        }

        ui.SetPercent(1f);
    }

    private static void ClearPreviousMap()
    {
        if (_previousMap.Scene.isLoaded)
        {
            Addressables.UnloadSceneAsync(_previousMap).Completed += (asyncHandle) =>
            {
                _previousMap = new SceneInstance();
            };
        }
    }
}