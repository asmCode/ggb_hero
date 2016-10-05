#if UNITY_ANDROID && !UNITY_EDITOR

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

        public void GetLocalUserScore(System.Action<Score> callback)
        {
            if (callback != null)
                callback(null);
        }
    }
}

#endif