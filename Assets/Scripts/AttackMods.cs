using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMods
{
    int numberOfProjectiles = 1;
    bool piercing = false;
    int numberOfPierces = 0;
    bool splashDamage = false;
    float splashDamageRadius = 5f;
    float splashDamageModifier = 0.25f;


}

public class Mod{
    string modifies;
    float floatValue;
    int intValue;

    public Mod(string modifies, float fv, int iv){
        
    }
}
