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

    public void ApplyMod(Mod mod){
        switch(mod.modifies){
            case "numProj":
                numberOfProjectiles += mod.intValue;
                break;
            case "pierce":
                piercing = true;
                break;
            case "numPierce":
                numberOfPierces += mod.intValue;
                break;
            case "splash":
                splashDamage = true;
                break;
            case "splashRadius":
                splashDamageRadius += mod.floatValue;
                break;
            case "splashMod":
                splashDamageModifier += mod.floatValue;
                break;
        }
    }
}

public class Mod{
    public string modifies;
    public float floatValue;
    public int intValue;

    public Mod(string modifies, float fv, int iv){

    }
}
