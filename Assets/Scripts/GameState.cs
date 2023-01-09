using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    public static bool selectingUpgrade = false;
    public static bool paused = false;

    public static bool SuppressUpdates(){
        return paused || selectingUpgrade;
    }
}
