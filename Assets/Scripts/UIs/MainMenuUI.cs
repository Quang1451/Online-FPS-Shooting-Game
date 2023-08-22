using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIBasic
{
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button LoadBtn;
    [SerializeField] private Button SettingBtn;
    [SerializeField] private Button AboutBtn;
    [SerializeField] private Button QuitBtn;

    public static string START = "START";

    private void OnEnable()
    {
        StartBtn?.onClick.AddListener(OnStart);
        LoadBtn?.onClick.AddListener(OnLoad);
        SettingBtn?.onClick.AddListener(OnSetting);
        AboutBtn?.onClick.AddListener(OnAbout);
        QuitBtn?.onClick.AddListener(OnQuit);
    }

    private void OnDisable()
    {
        StartBtn?.onClick.RemoveAllListeners();
        LoadBtn?.onClick.RemoveAllListeners();
        SettingBtn?.onClick.RemoveAllListeners();
        AboutBtn?.onClick.RemoveAllListeners();
        QuitBtn?.onClick.RemoveAllListeners();
    }

    #region Callback Methods
    private void OnStart()
    {
        ExecuteEvent(START);
    }

    private void OnLoad()
    {

    }

    private void OnSetting()
    {

    }

    private void OnAbout()
    {

    }

    private void OnQuit()
    {

    }
    #endregion
}
