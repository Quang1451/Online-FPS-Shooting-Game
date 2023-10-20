using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BaseView : MonoBehaviour, MVCIView
{
    public virtual void Initialize()
    {
    }
    public virtual void SpawnModel(Action action)
    {
    }

    public virtual void DoUpdate()
    {
    }
}
