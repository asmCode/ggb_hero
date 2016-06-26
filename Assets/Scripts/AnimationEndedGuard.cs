using UnityEngine;
using System.Collections;

public class AnimationEndedGuard : MonoBehaviour
{
    public event System.Action AnimationEnded;

    private void AnimationEvent_End()
    {
        if (AnimationEnded != null)
            AnimationEnded();
    }
}
