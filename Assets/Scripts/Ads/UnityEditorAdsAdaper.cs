using UnityEngine;

namespace Ssg.Ads
{
    public class UnityEditorAdsAdaper : IRewardedAd
    {
        public bool IsReady()
        {
            return true;
        }

        public bool Play(System.Action<AdFinishedEventArgs> adFinishedCallback)
        {
            Debug.Log("Play ad request: Fake Unity Editor.");

            if (adFinishedCallback != null)
                adFinishedCallback(new AdFinishedEventArgs(AdFinishedEventArgs.ResultType.FullyWatched));

            return true;
        }
    }
}