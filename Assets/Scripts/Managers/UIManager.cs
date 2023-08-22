using MEC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : SingletonDontDestroy<UIManager>
{
    public UIReferenceSO uiReferenceSO;

    public Transform canvasOverlay;
    public Transform dialogOverlay;

    public Transform canvasScreenSpace;
    public Transform dialogScreenSpace;

    private Dictionary<string, List<UIBasic>> _dictBaseUI;
    private Action _onUpdateEveryTime;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _dictBaseUI = new Dictionary<string, List<UIBasic>>();
    }

    #region Load Methods
    public IEnumerator<float> LoadAndShow<T>(string key, Transform parent = null, IUIShowData showData = null, IUIData data = null, Action<T> callback = null) where T : UIBasic
    {
        var uiRef = uiReferenceSO.GetUIReference(key);
        if (uiRef == null)
        {
            Debug.LogError($"[UI] LoadAndShow [{key}] is not exits");
            callback?.Invoke(null);
            yield break;
        }

        yield return Timing.WaitUntilDone(Timing.RunCoroutine(LoadAndShowAsync(key, parent, showData, data, callback)));
    }

    private IEnumerator<float> LoadAndShowAsync<T>(string key, Transform parent = null, IUIShowData showData = null, IUIData data = null, Action<T> callback = null, bool isShow = true) where T : UIBasic
    {
        var k = typeof(T).Name;
        UIBasic uiBasic = null;
        var uiRef = uiReferenceSO.GetUIReference(key);

        if (uiRef == null || string.IsNullOrEmpty(key))
        {
            Debug.LogError($"[UI] LoadAndShowAsync [{k}] is not exits");
            callback?.Invoke(null);
            yield break;
        }

        //check use in cache
        if (_dictBaseUI.ContainsKey(k) && _dictBaseUI[k] != null && _dictBaseUI[k].Count > 0)
        {
            if (uiRef.isSingle)
            {
                uiBasic = _dictBaseUI[k][0];
            }
            else
            {
                foreach (var ui in _dictBaseUI[k])
                {
                    if (ui != null && !ui.gameObject.activeInHierarchy)
                    {
                        uiBasic = ui;
                        break;
                    }
                }
            }

            if (uiBasic != null)
            {
                uiBasic.RegisterEvent(UI_EVENT_KEY.DESTROY, OnDestroyUI);
                
                uiBasic.SetData(data);
                yield return Timing.WaitForOneFrame;
                uiBasic.RectTransform.SetParent(GetParentTransform(parent, showData));
                uiBasic.RectTransform.localPosition = Vector3.zero;
                uiBasic.RectTransform.localRotation = Quaternion.identity;

                if (isShow)
                {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(ShowWithParam(uiBasic, showData)));
                }
                else
                {
                    yield return Timing.WaitUntilDone(Timing.RunCoroutine(HideWithParam(uiBasic)));
                }
            }
        }

        //load addressable
        if (uiBasic == null)
        {
            yield return Timing.WaitForOneFrame;
            var goHandle = uiRef.assetRef.InstantiateAsync(GetParentTransform(parent, showData));

            yield return Timing.WaitUntilTrue(() => goHandle.IsDone);
            if (goHandle.Result != null)
            {
                uiBasic = goHandle.Result.GetComponent<T>();
            }

            if (uiBasic != null)
            {
                uiBasic.RegisterEvent(UI_EVENT_KEY.DESTROY, OnDestroyUI);

                uiBasic.RectTransform.SetParent(GetParentTransform(parent, showData));
                yield return Timing.WaitForOneFrame;
                uiBasic.RectTransform.localPosition = Vector3.zero;
                uiBasic.RectTransform.localRotation = Quaternion.identity;
                if (!_dictBaseUI.ContainsKey(k))
                {
                    _dictBaseUI[k] = new List<UIBasic>() { uiBasic };
                }
                else
                {
                    _dictBaseUI[k].Add(uiBasic);
                }
            }
            else
            {
                callback?.Invoke(null);
                yield break;
            }

            if (!uiBasic.IsInited())
            {
                uiBasic.Initialize();
            }

            yield return Timing.WaitUntilTrue(() => uiBasic.IsInited());
            uiBasic.SetData(data);
            yield return Timing.WaitForOneFrame;

            if (isShow)
            {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(ShowWithParam(uiBasic, showData)));
            }
            else
            {
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(HideWithParam(uiBasic)));
            }
        }

        if (uiBasic is T outPut)
        {
            callback?.Invoke(outPut);
        }
        else
        {
            Debug.LogError($"[UI] LoadAndShow [{k}] is not match {typeof(T).Name}");
            callback?.Invoke(null);
        }
    }

    private IEnumerator<float> ShowWithParam(UIBasic uiBasic, IUIShowData showData = null)
    {
        if (uiBasic == null)
            yield break;
        if (!uiBasic.IsInited())
        {
            uiBasic.Initialize();
        }

        yield return Timing.WaitUntilTrue(uiBasic.IsInited);
        uiBasic.RectTransform.SetAsLastSibling();
        uiBasic.RectTransform.localPosition = Vector3.zero;
        uiBasic.Show(showData);
        yield return Timing.WaitForOneFrame;
    }

    private IEnumerator<float> HideWithParam(UIBasic uiBasic)
    {
        if (uiBasic == null)
            yield break;
        if (!uiBasic.IsInited())
        {
            uiBasic.Initialize();
        }

        yield return Timing.WaitUntilTrue(uiBasic.IsInited);
        uiBasic.RectTransform.SetAsLastSibling();
        uiBasic.RectTransform.localPosition = Vector3.zero;
        uiBasic.OnHideComplete();
        yield return Timing.WaitForOneFrame;
    }
    #endregion

    #region Hide And Destroy Methods
    public void HideUi(string key, bool all = true)
    {
        if (!_dictBaseUI.ContainsKey(key))
            return;

        if (!all)
        {
            foreach (var ui in _dictBaseUI[key].Where(ui => ui.isActiveAndEnabled))
            {
                ui.Hide();
                return;
            }
        }
            

        foreach (var ui in _dictBaseUI[key].Where(ui => ui != null))
        {
            ui.Hide();
        }
    }

    public void ClearAllUi()
    {
        Timing.RunCoroutine(IClearAllUI());
    }

    public IEnumerator<float> IClearAllUI()
    {
        var count = 0;
        foreach (var item in _dictBaseUI)
        {
            if (item.Value == null || item.Value.Count <= 0) continue;
            foreach (var ui in item.Value)
            {
                if (ui != null && ui.gameObject != null)
                {
                    Destroy(ui.gameObject);
                    count++;

                    if (count % 10 == 0)
                        yield return Timing.WaitForOneFrame;
                }
            }
        }

        yield return Timing.WaitForOneFrame;
        _dictBaseUI.Clear();
    }

    #endregion

    #region UpdateUI Motheds
    public void UpdateUI()
    {
        _onUpdateEveryTime?.Invoke();
        foreach (var keyValuePair in _dictBaseUI)
        {
            if (keyValuePair.Value != null && keyValuePair.Value.Count > 0)
            {
                foreach (var ui in keyValuePair.Value)
                {
                    if (ui != null)
                    {
                        ui.UpdateEverySeconds(1);
                    }
                }
            }
        }
    }
    #endregion

    #region Main Methods
    public T Get<T>(string key) where T : UIBasic
    {
        if (!_dictBaseUI.ContainsKey(key))
        {
            return null;
        }

        foreach (var ui in _dictBaseUI[key])
        {
            if (ui != null && ui.gameObject.activeInHierarchy)
            {
                return (T)ui;
            }
        }

        return null;
    }

    public void Unregister(string assetGuid)
    {
        if (_dictBaseUI.ContainsKey(assetGuid))
        {
            _dictBaseUI.Remove(assetGuid);
        }
    }

    public Dictionary<string, List<UIBasic>> GetAllUi()
    {
        return _dictBaseUI;
    }

    private Transform GetParentTransform(Transform parent, IUIShowData showData)
    {
        return parent != null ? parent : GetParentTransform(showData);
    }

    private Transform GetParentTransform(IUIShowData showData)
    {
        if (showData == null) return transform;
        return showData.ParentType switch
        {
            ParentType.Overlay => canvasOverlay,
            ParentType.ScreenSpace => canvasScreenSpace,
            ParentType.DialogOverlay => dialogOverlay,
            ParentType.DialogScreenSpace => dialogScreenSpace,
            _ => transform
        };
    }
    #endregion

    #region Action Methods
    private void OnDestroyUI(object obj)
    {
        if (obj != null && obj is UIBasic screen)
        {
            screen.UnRegisterEvent(UI_EVENT_KEY.DESTROY, OnDestroyUI);
            Destroy(screen.gameObject);
        }
    }

    public void RegisterUpdateEveryTime(Action action)
    {
        _onUpdateEveryTime += action;
    }
    
    public void UnRegisterUpdateEveryTime(Action action)
    {
        _onUpdateEveryTime -= action;
    }
    #endregion
}
