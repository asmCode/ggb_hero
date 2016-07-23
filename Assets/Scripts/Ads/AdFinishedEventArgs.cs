namespace Ssg.Ads
{
    public class AdFinishedEventArgs : System.EventArgs
    {
        public enum ResultType
        {
            FullyWatched,
            NotFullyWatched,
            Error
        }

        public ResultType Result { get; private set; }

        public AdFinishedEventArgs(ResultType result)
        {
            Result = result;
        }
    }
}