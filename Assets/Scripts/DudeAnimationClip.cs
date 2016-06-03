using UnityEngine;
using System.Collections;

public class DudeAnimationClip
{
    protected DudeAnimator DudeAnimator
    {
        get;
        private set;
    }

    public DudeAnimationClip(DudeAnimator dudeAnimator)
    {
        DudeAnimator = dudeAnimator;
    }

    public virtual void Update() { }
}
