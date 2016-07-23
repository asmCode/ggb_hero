using UnityEngine;

namespace Ssg.Ads
{
    public class VungleAdaper : IRewardedAd
    {
        private System.Action<AdFinishedEventArgs> m_currentAdFinishedCallback;

        public bool IsReady()
        {
            return Vungle.isAdvertAvailable();
        }

        public bool Play(System.Action<AdFinishedEventArgs> adFinishedCallback)
        {
            Debug.Log("Play ad request: Vungle.");

            m_currentAdFinishedCallback = adFinishedCallback;
            Vungle.onAdFinishedEvent += Vungle_onAdFinishedEvent;
            Vungle.playAdWithOptions(new System.Collections.Generic.Dictionary<string, object>());
            return true;
        }

        private void Vungle_onAdFinishedEvent(global::AdFinishedEventArgs obj)
        {
            Debug.Log("Vungle Ad Finished.");

            Vungle.onAdFinishedEvent -= Vungle_onAdFinishedEvent;

            if (m_currentAdFinishedCallback != null)
            {
                System.Action<AdFinishedEventArgs> tmp_cb = m_currentAdFinishedCallback;
                m_currentAdFinishedCallback = null;
                tmp_cb(new AdFinishedEventArgs(obj.IsCompletedView ? AdFinishedEventArgs.ResultType.FullyWatched : AdFinishedEventArgs.ResultType.NotFullyWatched));
            }
        }
    }
}