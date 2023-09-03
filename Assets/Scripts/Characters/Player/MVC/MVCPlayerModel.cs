using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerModel : BaseData
{
    public PlayerStateReusubleData reusubleData;
    public override void ApplyDesign()
    {
        reusubleData = new PlayerStateReusubleData();
    }    
}
