using UnityEngine;
using System.Collections;

public class GameSettings
{
    public static int HandCapacity
    {
        get;
        set;
    }

    public static float GravityPerSuicider
    {
        get;
        set;
    }

    public static int SuiDeathsLimit
    {
        get;
        set;
    }

    public static int SuiDeathsCount
    {
        get;
        set;
    }

    public static int SuiRescuedCount
    {
        get;
        set;
    }

    public static float SuiJumpDelayEasiest { get; private set; }
    public static float SuiJumpDelayHardest { get; private set; }
    public static float SuiJumpDelayHardestAfterTime { get; private set; }

    static GameSettings()
    {
        Restart();
    }

    public static void Restart()
    {
        HandCapacity = 3;
        GravityPerSuicider = 2.5f;
        SuiDeathsLimit = 3;
        SuiDeathsCount = 0;
        SuiRescuedCount = 0;

        SuiJumpDelayEasiest = 3.0f;
        SuiJumpDelayHardest = 1.6f;
        SuiJumpDelayHardestAfterTime = 4.0f * 60.0f;
    }
}
