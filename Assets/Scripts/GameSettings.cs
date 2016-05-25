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

    public static float SuiJumpDelayEasiestFirstWave { get; private set; }
    public static float SuiJumpDelayHardestFirstWave { get; private set; }
    public static float SuiJumpDelayEasiestLastWave { get; private set; }
    public static float SuiJumpDelayHardestLastWave { get; private set; }
    public static float WaveLengthFirstWave { get; private set; }
    public static float WaveLengthLastWave { get; private set; }
    public static int LastWave { get; private set; }

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

        SuiJumpDelayEasiestFirstWave = 2.5f;
        SuiJumpDelayHardestFirstWave = 2.0f;
        SuiJumpDelayEasiestLastWave = 2.0f;
        SuiJumpDelayHardestLastWave = 1.0f;
        WaveLengthFirstWave = 30.0f;
        WaveLengthLastWave = 90.0f;
        LastWave = 9;

        SuiJumpDelayEasiest = 2.0f;
        SuiJumpDelayHardest = 1.0f;
        SuiJumpDelayHardestAfterTime = 4.0f * 60.0f;
    }
}
