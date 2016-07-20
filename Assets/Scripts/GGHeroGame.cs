using UnityEngine;
using System.Collections;

public class GGHeroGame : MonoBehaviour
{
    private void Awake()
    {
        InitRewardedAds();
    }

    private void InitRewardedAds()
    {
        Debug.Log("initializing vungle");
        Vungle.setLogEnable(true);
#if UNITY_EDITOR
        RewardedAds.Add(new UnityEditorAdsAdaper());
#else
        Debug.Log("cyc");
        Vungle.init("578f0a3200e14959060000bb", "578f0a0be024f26c23000093");
        RewardedAds.Add(new VungleAdaper());
#endif
    }
}
