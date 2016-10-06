namespace Ssg.Social
{
    public class SocialImplIOS : ISocialImpl
    {
		private UnityEngine.SocialPlatforms.ILeaderboard m_leaderboardSuisSaved;

        public bool IsAuthenticated
        {
            get
            {
                NGUIDebug.Log("IsAuthenticated: " + UnityEngine.Social.localUser.authenticated.ToString());

				return UnityEngine.Social.localUser.authenticated;
            }
        }

        public void Authenticate(System.Action<bool> callback)
        {
            NGUIDebug.Log("Authenticate...");

            UnityEngine.Social.localUser.Authenticate((success) =>
            {
                NGUIDebug.Log("Authenticate result: " + success.ToString());

                if (callback != null)
                    callback(success);
            });
        }

        public void GetLocalUserScore(string leaderboardId, System.Action<Score> callback)
        {
            NGUIDebug.Log("GetLocalUserScore...");

            if (m_leaderboardSuisSaved == null)
			{
				m_leaderboardSuisSaved = UnityEngine.Social.CreateLeaderboard();
				m_leaderboardSuisSaved.id = leaderboardId;      
			}

			//m_leaderboardSuisSaved.SetUserFilter(new string[] {UnityEngine.Social.localUser.id});

            m_leaderboardSuisSaved.LoadScores(result =>
			{
	    		bool success = m_leaderboardSuisSaved != null && m_leaderboardSuisSaved.localUserScore != null;
                NGUIDebug.Log("GetLocalUserScore result: " + success.ToString());
                Score score = null;
				if (success)
				{
				    score = new Score();
                    score.Value = m_leaderboardSuisSaved.localUserScore.value;
                    score.Rank = m_leaderboardSuisSaved.localUserScore.rank;

                    NGUIDebug.Log("GetLocalUserScore score.value = " + score.Value);
                    NGUIDebug.Log("GetLocalUserScore score.rank = " + score.Rank);
                }

		        if (callback != null)
		        	callback(score);
			});
        }

        public void ReportLocalUserScore(string leaderboardId, long score, System.Action<bool> callback)
        {
            NGUIDebug.Log("ReportLocalUserScore...");

            UnityEngine.Social.ReportScore(score, leaderboardId, success =>
            {
                NGUIDebug.Log("ReportLocalUserScore result: " + success.ToString());

                if (callback != null)
                    callback(success);
			});
        }

        public void ShowLeaderboards()
        {
            UnityEngine.Social.ShowLeaderboardUI();
        }
    }
}