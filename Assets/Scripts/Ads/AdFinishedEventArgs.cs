namespace Ssg.Ads
{
    public class AdFinishedEventArgs : System.EventArgs
    {
        public enum ResultType
        {
            FullyWatched,
            Skipped,
            Error
        }

        public ResultType Result { get; private set; }

        public AdFinishedEventArgs(ResultType result)
        {
            Result = result;
        }
    }
}