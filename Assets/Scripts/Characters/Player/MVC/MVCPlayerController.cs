using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerController : BaseController
{
   public override void Initialize()
   {
      base.Initialize();
      _view.SpawnModel(OnLoadModelComplete);
      _data.ApplyDesign();
   }

   private void OnLoadModelComplete()
   {
      //_data.SetPosition(_view.transform.position);
      
   }
}
