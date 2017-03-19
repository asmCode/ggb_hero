using UnityEngine;
using System.Collections;

public class GameSettings
{
    public static bool Censore
    {
        get;
        private set;
    }

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

    public static float OnWaterSpeed { get; private set; }
    public static float SuiJumpDelayEasiestFirstWave { get; private set; }
    public static float SuiJumpDelayHardestFirstWave { get; private set; }
    public static float SuiJumpDelayEasiestLastWave { get; private set; }
    public static float SuiJumpDelayHardestLastWave { get; private set; }
    public static float WaveLengthFirstWave { get; private set; }
    public static float WaveLengthLastWave { get; private set; }
    public static int LastWave { get; private set; }
    public static float SuiFallingSpeedMin { get; private set; }
    public static float SuiFallingSpeedMax { get; private set; }

    static GameSettings()
    {
        Restart();
    }

    public static void Restart()
    {
        // Censore = Application.platform == RuntimePlatform.IPhonePlayer;
        Censore = false;

        HandCapacity = 10;
        GravityPerSuicider = 2.5f;
        SuiDeathsLimit = 3;
        SuiDeathsCount = 0;
        SuiRescuedCount = 0;

        OnWaterSpeed = 1.0f;

        SuiJumpDelayEasiestFirstWave = 1.8f;
        SuiJumpDelayHardestFirstWave = 1.5f;
        SuiJumpDelayEasiestLastWave = 1.3f;
        SuiJumpDelayHardestLastWave = 1.1f;
        WaveLengthFirstWave = 30.0f;
        WaveLengthLastWave = 90.0f;
        LastWave = 9;

        SuiFallingSpeedMin = 0.18f;
        SuiFallingSpeedMax = 0.40f;
    }
}
