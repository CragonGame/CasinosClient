using System;
using System.Diagnostics;
using GameCloud.Unity.Common;

public enum EbLogLevel
{
    OK,
    CRIT_OK,
    WARNING,
    ERROR,
    TOTAL,
}

public class EbColor
{
    //-------------------------------------------------------------------------
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }
    public float a { get; set; }

    //-------------------------------------------------------------------------
    public EbColor()
        : this(1, 1, 1, 1)
    {
        r = g = b = a = 1;
    }

    //-------------------------------------------------------------------------
    public EbColor(float r, float g, float b)
        : this(r, g, b, 1)
    {
    }

    //-------------------------------------------------------------------------
    public EbColor(float r, float g, float b, float a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public static EbColor black { get { return new EbColor(0, 0, 0, 1); } }
    public static EbColor blue { get { return new EbColor(0, 0, 1, 1); } }
    public static EbColor cyan { get { return new EbColor(0, 1, 1, 1); } }
    public static EbColor gray { get { return new EbColor(0.5f, 0.5f, 0.5f, 1); } }
    public static EbColor green { get { return new EbColor(0, 1, 0, 1); } }
    public static EbColor grey { get { return new EbColor(0.5f, 0.5f, 0.5f, 1); } }
    public static EbColor magenta { get { return new EbColor(1, 0, 1, 1); } }
    public static EbColor red { get { return new EbColor(1, 0, 0, 1); } }
    public static EbColor white { get { return new EbColor(1, 1, 1, 1); } }
    public static EbColor yellow { get { return new EbColor(1, 0.92f, 0.016f, 1); } }
}

public static class EbLog
{
    //-------------------------------------------------------------------------
    public delegate void OnPrintStringCallback(string msg);
    public delegate void OnDrawLineDelegate(EbVector3 start, EbVector3 end, EbColor color);
    public static OnPrintStringCallback NoteCallback { get; set; }
    public static OnPrintStringCallback CritOKCallback { get; set; }
    public static OnPrintStringCallback ErrorCallback { get; set; }
    public static OnPrintStringCallback WarningCallback { get; set; }
    public static OnDrawLineDelegate OnDrawLine { get; set; }
    public static EbLogLevel LogLevel = EbLogLevel.OK;

    //-------------------------------------------------------------------------
    public static void AssertError(bool condition)
    {
        if (condition == false)
        {
            _Error("<ERROR>");
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertError(bool condition, string log)
    {
        if (condition == false)
        {
            _Error(log);
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertWarning(bool condition)
    {
        if (condition == false)
        {
            _Warning("<WARNING>");
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertWarning(bool condition, string log)
    {
        if (condition == false)
        {
            _Warning(log);
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertError<T>(T ptr) where T : class
    {
        if (ptr == null)
        {
            _Error("<ERROR>");
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertError<T>(T ptr, string log)
    {
        if (ptr == null)
        {
            _Error(log);
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertWarning<T>(T ptr) where T : class
    {
        if (ptr == null)
        {
            _Warning("<WARNING>");
        }
    }

    //-------------------------------------------------------------------------
    public static void AssertWarning<T>(T ptr, string log)
    {
        if (ptr == null)
        {
            _Warning(log);
        }
    }

    //-------------------------------------------------------------------------
    public static void Error(string log)
    {
        _Error(log);
    }

    //-------------------------------------------------------------------------
    public static void Warning(string log)
    {
        _Warning(log);
    }

    //-------------------------------------------------------------------------
    public static void DrawLine(EbVector3 start, EbVector3 end, EbColor color)
    {
        OnDrawLine(start, end, color);
    }

    //-------------------------------------------------------------------------
    public static void DrawRay(EbVector3 origin, EbVector3 direct, EbColor color)
    {
        OnDrawLine(origin, origin + direct * 1000f, color);
    }

    //-------------------------------------------------------------------------
    public static void CritOK(string log)
    {
        if (LogLevel <= EbLogLevel.CRIT_OK)
        {
            if (CritOKCallback != null)
            {
                CritOKCallback(log);
            }
        }
    }

    //-------------------------------------------------------------------------
    public static void Note(string log)
    {
        if (LogLevel <= EbLogLevel.OK)
        {
            if (NoteCallback != null)
            {
                NoteCallback(log);
            }
        }
    }

    //-------------------------------------------------------------------------
    static void _Error(string log)
    {
        if (LogLevel <= EbLogLevel.ERROR)
        {
            if (ErrorCallback != null)
            {
                ErrorCallback(log);
            }
        }
    }

    //-------------------------------------------------------------------------
    static void _Warning(string log)
    {
        if (LogLevel <= EbLogLevel.WARNING)
        {
            if (WarningCallback != null)
            {
                WarningCallback(log);
            }
        }
    }
}
