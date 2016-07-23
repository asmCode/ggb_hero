using System.Collections.Generic;

namespace Ssg.Ads
{
    public class RewardedAds : MonoBehaviorSingleton<RewardedAds>
    {
        private List<IRewardedAd> m_ads = new List<IRewardedAd>();

        public void Add(IRewardedAd ad)
        {
            m_ads.Add(ad);
        }

        public bool IsReady()
        {
            return GetReadyAd() != null;
        }

        public bool Play(System.Action<AdFinishedEventArgs> adFinishedCallback)
        {
            IRewardedAd ad = GetReadyAd();
            if (ad == null)
                return false;

            return ad.Play(adFinishedCallback);
        }

        private RewardedAds() { }

        private IRewardedAd GetReadyAd()
        {
            foreach (var item in m_ads)
            {
                if (item.IsReady())
                    return item;
            }

            return null;
        }
    }
}