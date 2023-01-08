using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMods: MonoBehaviour
{
    public int numberOfProjectiles = 1;
    public bool piercing = false;
    public int numberOfPierces = 0;
    public bool splashDamage = false;
    public float splashDamageRadius = 5f;
    public float splashDamageModifier = 0.25f;

    public void ApplyMod(Mod mod){
        Debug.Log("Applying mod - " + mod.ToString());
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
    public int materialIndex;

    public Mod(string modifies, int matIndex, float fv = 0f, int iv = 0){
        this.modifies = modifies;
        floatValue = fv;
        intValue = iv;
        materialIndex = matIndex;
    }

    public string ToString(){
        return modifies + " " + floatValue + " " + intValue + " " + materialIndex;
    }
}

public static class ModPool{
    public static Mod[] GetMajorUpgrades1(){
        return new Mod[]{
            new Mod("splash", 0),
            new Mod("pierce", 1),
            new Mod("numProj", 2, 0f, 4)
        };
    }

    public static Mod[] GetModsForLevel(int level){
        if(level == 1) return GetMajorUpgrades1();
        else return new Mod[]{};
    }

}
