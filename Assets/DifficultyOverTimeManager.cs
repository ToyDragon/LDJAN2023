using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyOverTimeManager : MonoBehaviour
{
    static float timeSinceStart = 0f;

    void Update()
    {
        if(GameState.SuppressUpdates()) return;
        timeSinceStart += Time.deltaTime;
    }

    public static float GetDifficultyMultiplier(){
        return Mathf.Pow(2,timeSinceStart/45f) - 1f;
    }
}
