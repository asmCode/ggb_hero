class SocialIds
{
#if UNITY_EDITOR
    // Leaderboards
    public const string LeaderboardSuisSaved = "editor.suis_saved";
    public const string LeaderboardTotalSuisSaved = "editor.total_suis_saved";
    // Achievements
    public const string AchievementSave100InTotal = "editor.achievement_save_100_in_total";
    public const string AchievementSave200InTotal = "editor.achievement_save_200_in_total";
    public const string AchievementSave500InTotal = "editor.achievement_save_500_in_total";
    public const string AchievementSave1000InTotal = "editor.achievement_save_1000_in_total";
    public const string AchievementSave5000InTotal = "editor.achievement_save_5000_in_total";

#elif UNITY_IOS
    // Leaderboards
    public const string LeaderboardSuisSaved = "gghero.suis_saved";
    public const string LeaderboardTotalSuisSaved = "gghero.total_suis_saved";
    // Achievements
    public const string AchievementSave100InTotal = "gghero.achievement_save_100_in_total";
    public const string AchievementSave200InTotal = "gghero.achievement_save_200_in_total";
    public const string AchievementSave500InTotal = "gghero.achievement_save_500_in_total";
    public const string AchievementSave1000InTotal = "gghero.achievement_save_1000_in_total";
    public const string AchievementSave5000InTotal = "gghero.achievement_save_5000_in_total";

#elif UNITY_ANDROID
    // Leaderboards
    public const string LeaderboardSuisSaved = GPGSIds.leaderboard_suiciders_saved;
    public const string LeaderboardTotalSuisSaved = GPGSIds.leaderboard_total_suiciders_saved;
    // Achievements
    public const string AchievementSave100InTotal = GPGSIds.achievement_save_100_suiciders_in_total;
    public const string AchievementSave200InTotal = GPGSIds.achievement_save_200_suiciders_in_total;
    public const string AchievementSave500InTotal = GPGSIds.achievement_save_500_suiciders_in_total;
    public const string AchievementSave1000InTotal = GPGSIds.achievement_save_1000_suiciders_in_total;
    public const string AchievementSave5000InTotal = GPGSIds.achievement_save_5000_suiciders_in_total;
#endif
}
