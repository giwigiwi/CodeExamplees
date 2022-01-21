using System;
using UnityEngine;


public class Clock {

    public static DateTime minusDataTime = new DateTime(2018, 1, 1, 0, 0, 0, 0);
    public static int NowSecond
    {
        get
        {
            var dt = DateTime.Now;
            TimeSpan ts = dt.Subtract(minusDataTime);
            return (int)(ts.TotalSeconds);
        }
    }

    //public static float TotalMinute(DateTime dt)
    //{
    //    TimeSpan ts = new TimeSpan(dt.Ticks);
    //    return ts.TotalMinutes;
    //}

    public static int GetHours(double minute)
    {
        var ret = minute / 60;
        return Convert.ToInt32(ret);
    }

    public static int GetDay(double minute)
    {
        var ret = (minute / 60)/12;
        return Convert.ToInt32(ret);
    }

    public static int GetSecond(int second)
    {
        float ret = (second % 60);
        return Convert.ToInt32(ret);
    }
    public static int GetRealMinute(int second)
    {
        var m = ((second / 60f) % 60);
        return (int)m;
    }

    public static int GetRealHours(int second)
    {
        var m = second / 60f;
        //var h = ((m / 60f) % 24);
        return (int)m;
    }

    public static int GetHours(int second)
    {
        var m = second / 60f;
        var h = (m / 60f);
        
        return (int)h;
    }
}
