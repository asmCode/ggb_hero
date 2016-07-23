﻿using UnityEngine;
using Ssg.Ads;

public class GGHeroGame : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitRewardedAds();
    }

    private void InitRewardedAds()
    {
        Debug.Log("Initializing rewarded ads");
#if UNITY_EDITOR
        Debug.Log("Creating UnityEditorAdsAdaper");
        RewardedAds.GetInstance().Add(new UnityEditorAdsAdaper());
#else
        Debug.Log("Creating VungleAdaper");
        Vungle.init("578f0a3200e14959060000bb", "578f0a0be024f26c23000093");
        RewardedAds.Add(new VungleAdaper());
#endif
    }
}