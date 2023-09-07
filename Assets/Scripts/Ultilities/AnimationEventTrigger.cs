using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    private PlayerMovement _movement;
    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
    }

    public void OnAnimationExitEvent()
    {
        _movement?.OnAnimationExitEvent();
    }
}
