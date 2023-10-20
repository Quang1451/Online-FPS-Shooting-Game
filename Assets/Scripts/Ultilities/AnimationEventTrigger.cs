using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTrigger : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInventory _inventory;
    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
        _inventory = GetComponentInParent<PlayerInventory>();
    }

    public void OnAnimationExitEvent()
    {
        _movement?.OnAnimationExitEvent();
    }

    public void OnReloadFinish()
    {
        _inventory?.OnReloadFinish();
    }
}
