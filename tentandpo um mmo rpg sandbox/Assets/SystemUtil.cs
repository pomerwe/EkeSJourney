using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class SystemUtil
{

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);
}
