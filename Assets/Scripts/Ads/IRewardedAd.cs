namespace Ssg.Ads
{
    public interface IRewardedAd
    {
        bool IsReady();
        bool Play(System.Action<AdFinishedEventArgs> adFinishedCallback);
    }
}