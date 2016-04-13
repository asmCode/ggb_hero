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

    static GameSettings()
    {
        Restart();
    }

    public static void Restart()
    {
        HandCapacity = 3;
        GravityPerSuicider = 2.0f;
        SuiDeathsLimit = 3;
        SuiDeathsCount = 0;
        SuiRescuedCount = 0;
    }
}
