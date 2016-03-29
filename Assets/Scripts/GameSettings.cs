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

    static GameSettings()
    {
        HandCapacity = 1;
        GravityPerSuicider = 3.0f;
    }
}
