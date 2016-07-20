using UnityEngine;
using System.Collections;

public class VungleAdaper : IRewardedAd
{
    private System.Action m_currentAdFinishedCallback;

    public bool IsReady()
    {
        Debug.Log("isAdvertAvailable");
        return Vungle.isAdvertAvailable();
    }

    public bool Play(System.Action adFinishedCallback)
    {
        m_currentAdFinishedCallback = adFinishedCallback;
        Vungle.onAdFinishedEvent += Vungle_onAdFinishedEvent;
        Vungle.playAdWithOptions(new System.Collections.Generic.Dictionary<string, object>());
        return true;
    }

    private void Vungle_onAdFinishedEvent(AdFinishedEventArgs obj)
    {
        Debug.Log("Vungle Ad Finished");

        Vungle.onAdFinishedEvent -= Vungle_onAdFinishedEvent;

        if (m_currentAdFinishedCallback != null)
        {
            System.Action tmp_cb = m_currentAdFinishedCallback;
            m_currentAdFinishedCallback = null;
            tmp_cb();
        }
    }
}
