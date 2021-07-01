using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MobileChecker : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public static bool isMobile()
    {
        //return true; //descomentar para testear en el editor        
        #if !UNITY_EDITOR && UNITY_WEBGL
                return IsMobile();
        #endif
                return false;
    }
}