using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Singleton<MainMenu>
{
    /*[FilePath(Extensions = "json")] public string jsonFile;*/

    void Awake()
    {
        //Load LevelData
        /*LevelData.LoadData(jsonFile);*/

        //Clear All UI
        UIManager.Instance.ClearAllUi();
        Timing.RunCoroutine(ShowMainMenuUI());
        InputManager.EnableUIActions();
    }

    IEnumerator<float> ShowMainMenuUI()
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(UIManager.Instance.LoadAndShow<MainMenuUI>(UIKey.MAINMENUUI, null, new UIShowData
        {
            ParentType = ParentType.Overlay
        })));

        MainMenuUI mainMenu = UIManager.Instance.Get<MainMenuUI>(UIKey.MAINMENUUI);

        mainMenu?.RegisterEvent(MainMenuUI.START, StartGame);
    }

    private void StartGame(object obj)
    {
       /* LevelData.chooseLevel = LevelDataKey.Key1;*/
        InputManager.DisableBothActoins();

        Timing.RunCoroutine(StartGameHandle());
    }

    IEnumerator<float> StartGameHandle()
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(UIManager.Instance.LoadAndShow<LoadingUI>(UIKey.LOADINGUI,
            null, new UIShowData
            {
                ParentType = ParentType.Overlay
            })));
        LoadingUI loadingUI = UIManager.Instance.Get<LoadingUI>(UIKey.LOADINGUI);
        loadingUI.SetPercent(0f);

        Timing.RunCoroutine(SwitchSceneManager.LoadSceneAsync(SwitchSceneManager.GAMEPLAY, LoadSceneMode.Single,
            loadingUI));
    }
}