using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVCPlayerModel : BaseData
{
   public int speed;
   public int atk;
   
   public override void ApplyDesign()
   {
      //apply tu game design
      speed = 10;
      atk = 10;
   }
}
