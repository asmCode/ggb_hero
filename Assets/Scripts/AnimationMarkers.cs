using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationMarkers : MonoBehaviour
{
    public string m_animationClipName;

    public float[] Markers
    {
        get;
        private set;
    }

    private void Awake()
    {
        List<float> markers = new List<float>();

        Animator animator = GetComponent<Animator>();
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == m_animationClipName)
            {
                foreach (var animEvent in clip.events)
                {
                    if (animEvent.functionName == "Marker")
                    {
                        markers.Add(animEvent.time);
                    }
                }
            }
        }

        Markers = markers.ToArray();
    }

    public void Marker()
    {
    }
}
