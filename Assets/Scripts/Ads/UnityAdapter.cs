using UnityEngine;
using UnityEngine.Advertisements;

namespace Ssg.Ads
{
    public class UnityAdapter : IRewardedAd
    {
        private System.Action<AdFinishedEventArgs> m_currentAdFinishedCallback;

        public bool IsReady()
        {
            return Advertisement.IsReady();
        }

        public bool Play(System.Action<AdFinishedEventArgs> adFinishedCallback)
        {
            Debug.Log("Play ad request: Unity.");

            m_currentAdFinishedCallback = adFinishedCallback;
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(null, options);

            return true;
        }

        private void HandleShowResult(ShowResult result)
        {
            if (m_currentAdFinishedCallback != null)
            {
                AdFinishedEventArgs.ResultType AdFinishedEventArgsResult = AdFinishedEventArgs.ResultType.Error;

                switch (result)
                {
                    case ShowResult.Finished:
                        AdFinishedEventArgsResult = AdFinishedEventArgs.ResultType.FullyWatched;
                        break;
                    case ShowResult.Skipped:
                        AdFinishedEventArgsResult = AdFinishedEventArgs.ResultType.Skipped;
                        break;
                    case ShowResult.Failed:
                        AdFinishedEventArgsResult = AdFinishedEventArgs.ResultType.Error;
                        break;
                }

                m_currentAdFinishedCallback(new AdFinishedEventArgs(AdFinishedEventArgsResult));
            }
        }
    }
}