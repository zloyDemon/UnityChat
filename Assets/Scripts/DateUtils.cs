using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DateUtils
{
    private const string DayFormat = "MM/dd/yyyy";
    private const string TodayTimeFormat = "HH:mm";

    public static string GetDateNowFormat()
    {
        var nowDate = GetDateNow();
        return nowDate.ToString(TodayTimeFormat);
    }

    public static DateTime GetDateNow()
    {
        return DateTime.Now;
    }
}
