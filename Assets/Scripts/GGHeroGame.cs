using UnityEngine;
using Ssg.Ads;

public class GGHeroGame : MonoBehaviour
{
    public const bool Debug = true;

    static bool m_isInitialized = false;

    private void Awake()
    {
        if (m_isInitialized)
            return;

        InitRewardedAds();

        Application.targetFrameRate = 300;

        m_isInitialized = true;
    }

    private void InitRewardedAds()
    {
        UnityEngine.Debug.Log("Initializing rewarded ads");
#if UNITY_EDITOR
        UnityEngine.Debug.Log("Creating UnityEditorAdsAdaper");
        RewardedAds.GetInstance().Add(new UnityEditorAdsAdaper());
#else
        UnityEngine.Debug.Log("Creating VungleAdaper");
        Vungle.init("578f0a3200e14959060000bb", "578f0a0be024f26c23000093");
        RewardedAds.GetInstance().Add(new VungleAdaper());

        UnityEngine.Debug.Log("Creating UnityAdaper");
        RewardedAds.GetInstance().Add(new UnityAdapter());
#endif
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Vungle.onPause();
        }
        else
        {
            Vungle.onResume();
        }
    }
}
