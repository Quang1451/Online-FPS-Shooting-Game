using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MVCPlayerModel : BaseData
{
    public PlayerStateReusubleData reusubleData;
    
    public MVCPlayerModel()
    {
        reusubleData = new PlayerStateReusubleData();
    }
}
