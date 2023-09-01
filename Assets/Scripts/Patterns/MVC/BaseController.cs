using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MVCIController
{
    protected BaseData _data;
    protected BaseView _view;
    
    public virtual void SetModel(MVCIData data)
    {
        _data = (BaseData)data;
    }

    public virtual void SetView(MVCIView view)
    {
        _view = (BaseView)view;
    }

    public virtual void Initialize()
    {
        _view.Initialize();
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }
}
