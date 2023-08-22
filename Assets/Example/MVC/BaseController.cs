using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MVCController
{
    protected BaseData _data;
    protected BaseView _view;
    
    public virtual void SetModel(MVCIData data)
    {
        _data = (BaseData)data;
    }

    public virtual void SetView(MVCView view)
    {
        _view = (BaseView)view;
    }

    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }

    public virtual void LateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
