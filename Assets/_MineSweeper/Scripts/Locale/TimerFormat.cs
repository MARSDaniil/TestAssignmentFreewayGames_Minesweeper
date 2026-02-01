using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimerFormat {
    static string[] TimeIds = { "d", "h", "m", "s" };

    public static string OneDigitTime(double Seconds) {
        string output = "";
        TimeSpan sec = TimeSpan.FromSeconds(Seconds);

        if (sec.Days != 0) {
            output = $"{sec.Days}{TimeIds[0]}";
        } else if (sec.Hours != 0) {
            output = $"{sec.Hours}{TimeIds[1]}";
        } else if (sec.Minutes != 0) {
            output = $"{sec.Minutes}{TimeIds[2]}";
        } else {
            output = $"{sec.Seconds}{TimeIds[3]}";
        }

        return output;
    }

    public static string ShortTime_ZeroesFree(double Seconds) {
        string output = "";
        TimeSpan sec = TimeSpan.FromSeconds(Seconds);

        if (sec.Days != 0) {
            output = $"{sec.Days}{TimeIds[0]} {sec.Hours}{TimeIds[1]}";
        } else if (sec.Hours != 0) {
            output = $"{sec.Hours}{TimeIds[1]} {sec.Minutes}{TimeIds[2]}";
        } else {
            output = $"{sec.Minutes}{TimeIds[2]} {sec.Seconds}{TimeIds[3]}";
        }

        return output;
    }


    public static string ShortTime(double seconds) {
        return ShortTime(TimeSpan.FromSeconds(seconds), TimeIds);
    }

    public static string ShortTime(TimeSpan sec, string[] timePostfixes) {
        string output = "";
        if (sec.Days != 0) {
            output = string.Format("{0:D2}{1:D1} {2:D2}{3:D1}",
                sec.Days, timePostfixes[0], sec.Hours, timePostfixes[1]);
        } else if (sec.Hours != 0) {
            output = string.Format("{0:D2}{1:D1} {2:D2}{3:D1}",
                sec.Hours, timePostfixes[1], sec.Minutes, timePostfixes[2]);
        } else {
            output = string.Format("{0:D2}{1:D1} {2:D2}{3:D1}",
                sec.Minutes, timePostfixes[2], sec.Seconds, timePostfixes[3]);
        }
        return output;
    }

    public static string LongTime(double seconds) {
        return LongTime(TimeSpan.FromSeconds(seconds), TimeIds);
    }

    public static string LongTime(TimeSpan sec, string[] timePostfixes) {
        string formatStr = sec.Days == 0 ? "{2:D2}{3:D1} {4:D2}{5:D1} {6:D2}{7:D1}" :
            "{0:D2}{1:D1} {2:D2}{3:D1} {4:D2}{5:D1} {6:D2}{7:D1}";

        return string.Format(formatStr, sec.Days, timePostfixes[0], sec.Hours, timePostfixes[1],
            sec.Minutes, timePostfixes[2], sec.Seconds, timePostfixes[3]);
    }

    public static void SetTimePostfixes(string[] newTimeIds) {
        if (newTimeIds != null && newTimeIds.Length > 3) {
            TimeIds = newTimeIds;
        }
    }

    public static string TimeWithoutLocale(double seconds) {
        TimeSpan TS = TimeSpan.FromSeconds(seconds);
        string output = "";
        if (TS.Days != 0) {
            output = string.Format("{0:D2}{1:D1}{2:D2}{3:D1}{4:D2}{5:D1}{6:D2}",
                TS.Days, ":", TS.Hours, ":", TS.Minutes, ":", TS.Seconds);
        } else if (TS.Hours != 0) {
            output = string.Format("{0:D2}{1:D1}{2:D2}{3:D1}{4:D2}",
                TS.Hours, ":", TS.Minutes, ":", TS.Seconds);
        } else {
            output = string.Format("{0:D2}{1:D1}{2:D2}",
                TS.Minutes, ":", TS.Seconds);
        }
        return output;
    }
}
