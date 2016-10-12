class SocialIds
{
#if UNITY_EDITOR
    public const string LeaderboardSuisSaved = "editor.suis_saved";
    public const string LeaderboardTotalSuisSaved = "editor.total_suis_saved";
#elif UNITY_IOS
    public const string LeaderboardSuisSaved = "gghero.suis_saved";
    public const string LeaderboardTotalSuisSaved = "gghero.total_suis_saved";
#elif UNITY_ANDROID
    public const string LeaderboardSuisSaved = GPGSIds.leaderboard_suiciders_saved;
    public const string LeaderboardTotalSuisSaved = GPGSIds.leaderboard_total_suiciders_saved;
#endif
}
