using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardedAds
{
    private static List<IRewardedAd> m_ads = new List<IRewardedAd>();

    public static void Add(IRewardedAd ad)
    {
        m_ads.Add(ad);
    }

    public static bool IsReady()
    {
        return GetReadyAd() != null;
    }

    public static bool Play(System.Action adFinishedCallback)
    {
        IRewardedAd ad = GetReadyAd();
        if (ad == null)
            return false;

        return ad.Play(adFinishedCallback);
    }

    private static IRewardedAd GetReadyAd()
    {
        foreach (var item in m_ads)
        {
            if (item.IsReady())
                return item;
        }

        return null;
    }
}
