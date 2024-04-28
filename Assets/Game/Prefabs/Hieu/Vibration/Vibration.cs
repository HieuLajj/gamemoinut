////////////////////////////////////////////////////////////////////////////////
//
// @author Benoît Freslon @benoitfreslon
// https://github.com/BenoitFreslon/Vibration
// https://benoitfreslon.com
//
////////////////////////////////////////////////////////////////////////////////

using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

#if UNITY_IOS
using System.Collections;
using System.Runtime.InteropServices;
#endif

public static class Vibration
{

#if UNITY_IOS
    [DllImport ( "__Internal" )]
    private static extern bool _HasVibrator ();

    [DllImport ( "__Internal" )]
    private static extern void _Vibrate ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePop ();

    [DllImport ( "__Internal" )]
    private static extern void _VibratePeek ();

    [DllImport ( "__Internal" )]
    private static extern void _VibrateNope ();

    [DllImport("__Internal")]
    private static extern void _impactOccurred(string style);

    [DllImport("__Internal")]
    private static extern void _notificationOccurred(string style);

    [DllImport("__Internal")]
    private static extern void _selectionChanged();
#endif

#if UNITY_ANDROID
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
    public static AndroidJavaObject context;

    public static AndroidJavaClass vibrationEffect;


#endif

    private static bool initialized = false;
    public static void Init()
    {
        if (initialized) return;
        initialized = true;
    }


    public static void VibrateIOS(ImpactFeedbackStyle style)
    {
#if UNITY_IOS
        _impactOccurred(style.ToString());
#endif
    }

    public static void VibrateIOS(NotificationFeedbackStyle style)
    {
#if UNITY_IOS
        _notificationOccurred(style.ToString());
#endif
    }

    public static void VibrateIOS_SelectionChanged()

    {
#if UNITY_IOS
        _selectionChanged();
#endif
    }


    ///<summary>
    /// Tiny pop vibration
    ///</summary>
    public static void Vibrate()
    {

#if UNITY_IOS
        try
        {
            _VibratePop();
        }
        catch
        {
        }
       
#elif UNITY_ANDROID
        Vibrator.Vibrate(50);
#endif
    }
}


public enum ImpactFeedbackStyle
{
    Heavy,
    Medium,
    Light,
    Rigid,
    Soft
}

public enum NotificationFeedbackStyle
{
    Error,
    Success,
    Warning
}
