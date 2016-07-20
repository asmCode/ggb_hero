using UnityEngine;
using System.Collections;

public class UnityEditorAdsAdaper : IRewardedAd
{
    public bool IsReady()
    {
        return true;
    }

    public bool Play(System.Action adFinishedCallback)
    {
        if (adFinishedCallback != null)
            adFinishedCallback();

        return true;
    }
}
