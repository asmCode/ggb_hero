namespace Ssg.Social
{
    public class Social : MonoBehaviourSingleton<Social>
    {
        private ISocialImpl m_impl;

        public bool IsAuthenticated
        {
            get
            {
                if (m_impl != null)
                    return m_impl.IsAuthenticated;
                else
                    return false;
            }
        }

        public void Authenticate(System.Action<bool> callback)
        {
            if (m_impl != null)
                m_impl.Authenticate(callback);
        }

        public void GetLocalUserScore(string leaderboardId, System.Action<Score> callback)
        {
            if (m_impl != null)
                m_impl.GetLocalUserScore(leaderboardId, callback);
        }

        public void ReportLocalUserScore(string leaderboardId, long score, System.Action<bool> callback)
        {
            if (m_impl != null)
                m_impl.ReportLocalUserScore(leaderboardId, score, callback);
        }

        protected override void Awake()
        {
            CreateImplInstance();
        }

        private void CreateImplInstance()
        {
#if UNITY_EDITOR
            m_impl = new SocialImplEditor();
#elif UNITY_IOS
            m_impl = new SocialImplIOS();
#elif UNITY_ANDROID
            m_impl = new SocialImplGP();
#endif
        }
    }
}
