using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MVCPlayerView : BaseView
{
    public override void SpawnModel(System.Action action)
    {
        //spawn model truc tiep tu prefab
        action?.Invoke();
        
        //load async
        Addressables.InstantiateAsync("kay", transform.position, Quaternion.identity).Completed += handle =>
        {
            action?.Invoke();
        };
    }
}
