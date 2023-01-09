using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyOverTimeManager : MonoBehaviour
{
    float timeSinceStart = 0f;
    public static DifficultyOverTimeManager instance;
    void OnEnable() {
        instance = this;
    }
    void Update()
    {
        if(GameState.SuppressUpdates()) return;
        timeSinceStart += Time.deltaTime;
    }

    public static float GetDifficultyMultiplier(){
        return Mathf.Pow(2,instance.timeSinceStart/45f) - 1f;
    }
}
