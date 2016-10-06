namespace Ssg.Social
{
    public class SocialImplGP : ISocialImpl
    {
        public bool IsAuthenticated
        {
            get
            {
                return false;
            }
        }

        public void Authenticate(System.Action<bool> callback)
        {
            if (callback != null)
                callback(false);
        }

        public void GetLocalUserScore(string leaderboardId, System.Action<Score> callback)
        {
            if (callback != null)
                callback(null);
        }

        public void ReportLocalUserScore(string leaderboardId, long score, System.Action<bool> callback)
        {
            if (callback != null)
                callback(false);
        }

        public void ShowLeaderboards()
        {
        }
    }
}
