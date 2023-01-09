using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCheats : MonoBehaviour
{
    #if DEBUG
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) {
            Debug.Log("CHEAT level up");
            XPLevelController.instance.AddXP(1 + XPLevelController.instance.requiredXP - XPLevelController.instance.xp);
        }
    }
    #endif
}
