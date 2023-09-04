using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class MVCPlayerModel : BaseData
{
    public PlayerSO playerSO;
    public PlayerStateReusubleData reusubleData;
    public override void ApplyDesign(Action callback = null)
    {
        reusubleData = new PlayerStateReusubleData();
        Addressables.LoadAssetAsync<PlayerSO>("Data/PlayerSO").Completed += DataSO =>
        {
            playerSO = DataSO.Result;
            base.ApplyDesign(callback);
        };
    }    
}
