using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBasic : MonoBehaviour, IBaseUI, IUIBaseTransform
{
    public bool allowDestroyWhenHide;

    protected IUIShowData _showData;
    protected IUIHideData _hideData;
    
    protected Dictionary<string, Action<object>> _actions = new Dictionary<string, Action<object>>();

    private Transform _transform;
    private RectTransform _rectTransform;
    private bool _isInited;
    private bool _isActive;


    #region IUIBaseTransform Methods
    public Transform Transform
    {
        get
        {
            if (_transform == null)
            {
                _transform = gameObject.GetComponent<Transform>();
            }

            return _transform;
        }
    }

    public RectTransform RectTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = gameObject.GetComponent<RectTransform>();
            }

            return _rectTransform;
        }
    }
    #endregion

    #region IBaseUI Methods
    public virtual void Initialize()
    {
        _isInited = true;
    }

    public virtual void SetDefault()
    {
    }

    public virtual void SetData(IUIData data = null)
    {
    }

    public virtual void UpdateEverySeconds(float timeUpdate = 0)
    {
    }

    //Show Methods    
    public virtual void Show(IUIShowData showData = null)
    {
        gameObject.SetActive(true);
        _isActive = true;
        _showData = showData;
        BeginShow();
    }

    public virtual void BeginShow()
    {
        ExecuteEvent(UI_EVENT_KEY.BEGIN_SHOW);
    }

    public virtual void OnShowComplete()
    {
        ExecuteEvent(UI_EVENT_KEY.SHOW_COMPLETE);
    }

    //Hide Methods
    public virtual void Hide(IUIHideData hideData = null)
    {
        _isActive = false;
        _hideData = hideData;
        BeginHide();
    }

    public virtual void BeginHide()
    {
        ExecuteEvent(UI_EVENT_KEY.BEGIN_HIDE);
    }

    public virtual void OnHideComplete()
    {
        ExecuteEvent(UI_EVENT_KEY.HIDE_COMPLETE);
        if (allowDestroyWhenHide)
        {
            ExecuteEvent(UI_EVENT_KEY.DESTROY, this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    
    //Event Methods
    public virtual void RegisterEvent(string key, Action<object> evt)
    {
        _actions[key] = evt;
    }

    public virtual void UnRegisterEvent(string key, Action<object> evt)
    {
        if (_actions.ContainsKey(key))
        {
            _actions.Remove(key);
        }
    }
    #endregion

    #region Main Methods
    protected void ExecuteEvent(string key, object obj = null)
    {
        if (_actions.ContainsKey(key))
        {
            _actions[key]?.Invoke(obj);
        }
    }

    public bool IsInited()
    {
        return _isInited;
    }

    public bool IsActive()
    {
        return _isActive;
    }
    #endregion
}

public class UIShowData : IUIShowData
{
    public ParentType ParentType { get; set; }
}