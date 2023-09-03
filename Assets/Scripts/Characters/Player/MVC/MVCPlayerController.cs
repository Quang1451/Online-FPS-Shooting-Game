using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerController : BaseController
{
    private IController _controller;

    public override void Initialize()
    {
        base.Initialize();

        _view.SpawnModel(OnLoadModelComplete);
        _data.ApplyDesign();
        _controller = _view.GetComponent<PlayerController>();
    }

    private void OnLoadModelComplete()
    {
    }

    public override void Update()
    {
        _controller?.DoUpdate();
    }

    public override void FixedUpdate()
    {
        _controller?.DoFixedUpdate();
    }
}
