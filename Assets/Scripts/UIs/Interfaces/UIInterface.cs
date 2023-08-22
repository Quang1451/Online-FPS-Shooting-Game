using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParentType
{
    Overlay,
    DialogOverlay,

    ScreenSpace,
    DialogScreenSpace
}

public interface IUIData
{
}


public interface IUIShowData
{
    ParentType ParentType { get; set; }
}

public interface IUIHideData
{
}

public interface IUIBaseTransform
{
    public Transform Transform { get; }
    public RectTransform RectTransform { get; }
}

public interface IBaseUI
{
    void Initialize();
    void SetDefault();

    //data
    void SetData(IUIData data = null);
    void UpdateEverySeconds(float timeUpdate = 0);

    //show
    void Show(IUIShowData showData = null);
    void BeginShow();
    void OnShowComplete();

    //hide
    void Hide(IUIHideData hideData = null);
    void BeginHide();
    void OnHideComplete();

    //event
    void RegisterEvent(string key, Action<object> evt);
    void UnRegisterEvent(string key, Action<object> evt);
}

public class UI_EVENT_KEY
{
    public static string SHOW_COMPLETE = "SHOW_COMPLETE";
    public static string HIDE_COMPLETE = "HIDE_COMPLETE";
    public static string BEGIN_HIDE = "BEGIN_HIDE";
    public static string BEGIN_SHOW = "BEGIN_SHOW";
    public static string DESTROY = "DESTROY";
}