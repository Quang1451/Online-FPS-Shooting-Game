using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; 

    public void CrossFade(string animation, float durationTime)
    {
        animator?.CrossFade(animation, durationTime);
    }

    public void CrossFade(int hash, float durationTime)
    {
        animator?.CrossFade(hash, durationTime);
    }
}
