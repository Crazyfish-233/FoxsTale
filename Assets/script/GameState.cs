using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    public static bool IsPaused { get; set; }
    public static bool InputBlocked { get; set; }
    public static bool CanControlPlayer => !IsPaused && !InputBlocked;

    public static void Reset()
    {
        IsPaused = false;
        InputBlocked = false;
    }
}
