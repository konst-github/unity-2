
using UnityEngine;

public class Utils
{
    public class Angles
    {
        public const float Deg_30 = 30f;
        public const float Deg_45 = 45f;
        public const float Deg_60 = 60f;
        public const float Deg_90 = 90f;
        public const float Deg_180 = 180f;
        public const float Deg_360 = 360f;

        public const float Deg_Circle = Deg_360;
        public const float Deg_Half_Circle = Deg_Circle / 2;

        public static float RadiansToDegrees(float radians)
        {
            return radians / (Mathf.PI / Deg_180);
        }

        public static float DegreesToRadians(float degrees)
        {
            return degrees * (Mathf.PI / Deg_180);
        }
    }

    public class Random
    {
        public static float Sign()
        {
            return UnityEngine.Random.value >= 0.5 ? 1 : -1;
        }

        public static bool Bool()
        {
            return UnityEngine.Random.value >= 0.5;
        }
    }

    public class Clock
    {
        public const float Secs_per_Min = 60f;
        public const float Mins_per_Hr = 60f;
        public const float Secs_per_Hr = Secs_per_Min * Mins_per_Hr;
        public const float Hrs_Half_Day = 12f;
        public const float Hrs_Day = Hrs_Half_Day * 2;

        public const float Deg_per_Hr = Angles.Deg_Circle/ Hrs_Half_Day;
        public const float Deg_per_Min = Angles.Deg_Circle/ Mins_per_Hr;
        public const float Deg_per_Sec = Angles.Deg_Circle/ Secs_per_Min;
    }
}
