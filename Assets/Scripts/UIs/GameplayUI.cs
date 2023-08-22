using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GameplayUI : UIBasic
{
    [Header("References:")]
    public GameObject GameplayMenu;
    public GameObject PauseMenu;
    

    [Header("Buttons:")]
    public Button PauseBtn;
    public Button ResumeBtn;
    public Button SettingBtn;
    public Button QuitBtn;

    public static string PAUSE = "PAUSE";
    public static string RESUME = "RESUME";
    public static string QUIT = "QUIT";


    private void OnEnable()
    {
        PauseBtn?.onClick.AddListener(OnPause);
        ResumeBtn?.onClick.AddListener(OnResume);
        SettingBtn?.onClick.AddListener(OnSetting);
        QuitBtn?.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        PauseBtn?.onClick.RemoveAllListeners();
        ResumeBtn?.onClick.RemoveAllListeners();
        SettingBtn?.onClick.RemoveAllListeners();
        QuitBtn?.onClick.RemoveAllListeners();
    }

    #region Main Methods
   
    #endregion

    #region Callback Methods
    private void OnPause()
    {
        ExecuteEvent(PAUSE);
        GameplayMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }

    private void OnResume()
    {
        ExecuteEvent(RESUME);
        GameplayMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }

    private void OnSetting()
    {

    }

    private void OnQuit()
    {
        ExecuteEvent(QUIT);
    }
    #endregion

}
