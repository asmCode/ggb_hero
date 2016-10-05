﻿namespace Ssg.Social
{
    public class SocialImplEditor : ISocialImpl
    {
        private bool m_isAuthenticated = false;

        public bool IsAuthenticated
        {
            get
            {
                return m_isAuthenticated;
            }
        }

        public void Authenticate(System.Action<bool> callback)
        {
            m_isAuthenticated = false;
            if (callback != null)
                callback(m_isAuthenticated);
        }

        public void GetLocalUserScore(string leaderboardId, System.Action<Score> callback)
        {
            Score score = new Score();
            score.PlayerName = "Majak";
            score.Value = 231;
            score.Rank = UnityEngine.Random.Range(1, 100000);

            if (callback != null)
                callback(score);
        }

        public void ReportLocalUserScore(string leaderboardId, long score, System.Action<bool> callback)
        {
            if (callback != null)
                callback(true);
        }
    }
}
