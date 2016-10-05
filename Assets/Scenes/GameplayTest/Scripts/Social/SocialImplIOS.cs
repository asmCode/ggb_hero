#if UNITY_IOS && !UNITY_EDITOR

namespace Ssg.Social
{
    public class SocialImplIOS : ISocialImpl
    {
        private UnityEngine.SocialPlatforms.Social.ILeaderboard m_leaderboardSuisSaved;

        public bool IsAuthenticated
        {
            get
            {
                return UnityEngine.SocialPlatforms.Social.localUser.authenticated;
            }
        }

        public void Authenticate(System.Action<bool> callback)
        {
            
        }

        public void GetLocalUserScore(string leaderboardId, System.Action<Score> callback)
        {
if (m_leaderboardSuisSaved == null)
{
m_leaderboardSuisSaved = Social.CreateLeaderboard();
		    m_leaderboardSuisSaved.id = leaderboardId;
            
}

m_leaderboardSuisSaved.SetUserFilter(new string[] { UnityEngine.SocialPlatforms.Social.localUser.id });
            
		    m_leaderboardSuisSaved.LoadScores(result => {
    bool success = m_leaderboardSuisSaved != null && m_leaderboardSuisSaved.scores != null && m_leaderboardSuisSaved.scores.Length == 1;
    Score score = null;
if (success)
{
    score = new Score();
score.Value = m_leaderboardSuisSaved.scores[0].value;
score.Rank = m_leaderboardSuisSaved.scores[0].rank;
}
        if (callback != null)
                callback(score);
            });
        }

        public void ReportLocalUserScore(string leaderboardId, long score, System.Action<bool> callback)
        {
            UnityEngine.SocialPlatforms.Social.ReportScore(score, leaderboardID, success =>
            {
                if (callback != null)
                    callback(success);
			});
        }
    }
}

#endif