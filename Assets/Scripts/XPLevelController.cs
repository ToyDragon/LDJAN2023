using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPLevelController : MonoBehaviour
{
    public int level;
    public float xp;
    public float requiredXP;
    public float levelXPMultiplier = 1.3f;
    public static XPLevelController instance;
    public delegate void OnLevelUpDelegate();
    public OnLevelUpDelegate onLevelUp;
    void OnEnable() {
        instance = this;
    }
    public void AddXP(float amt) {
        xp += amt;
        while (xp > requiredXP) {
            level++;
            xp -= requiredXP;
            levelXPMultiplier *= levelXPMultiplier;
            if (onLevelUp != null) {
                onLevelUp();
            }
        }
    }
}
