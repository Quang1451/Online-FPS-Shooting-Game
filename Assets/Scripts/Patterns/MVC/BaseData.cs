using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseData : MVCIData
{
    public virtual void ApplyDesign(Action callback = null)
    {
        callback?.Invoke();
    }
}
