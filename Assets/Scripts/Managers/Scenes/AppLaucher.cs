using MEC;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AppLaucher : MonoBehaviour
{
    private void Start()
    {
        Initialize();
        Timing.RunCoroutine(LoadAssets());
    }

    private void Initialize()
    {
        UIManager.Instance.Initialize();
        AudioManager.Instance.Initialize();
        InputManager.Initialize();
    }


    IEnumerator<float> LoadAssets()
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(UIManager.Instance.LoadAndShow<LoadingUI>(UIKey.LOADINGUI, null, new UIShowData
        {
            ParentType = ParentType.Overlay
        })));
        LoadingUI loadingUI = UIManager.Instance.Get<LoadingUI>(UIKey.LOADINGUI);
        loadingUI.SetPercent(0f);

        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadUIs()));

        Timing.RunCoroutine(SwitchSceneManager.LoadSceneAsync(SwitchSceneManager.MAINMENU, LoadSceneMode.Single, loadingUI));
    }

    IEnumerator<float> LoadUIs()
    {
        //TODO load UI form load fucntion of UIManager and gameobj form 
        yield break;
    }

}
