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
    public float attackCooldownFast = 0.25f;
    public float attackCooldownSlow = .5f;
    public float attackSpeedMultiplier = 1.0f;
    public float moveSpeedMultiplier = 1.0f;
    public float projectileRange = 2.5f;

    public void ApplyMod(Mod mod){
        Debug.Log("Applying mod - " + mod.ToString());
        switch(mod.modifies){
            case "numProj":
                numberOfProjectiles += mod.intValue;
                break;
            case "pierce":
                piercing = true;
                numberOfPierces += mod.intValue;
                break;
            case "numPierce":
                numberOfPierces += mod.intValue;
                break;
            case "splash":
                splashDamage = true;
                splashDamageRadius += mod.floatValue;
                break;
            case "splashRadius":
                splashDamageRadius += mod.floatValue;
                break;
            case "splashMod":
                splashDamageModifier += mod.floatValue;
                break;
            case "attackSpeed":
                attackSpeedMultiplier -= mod.floatValue;
                break;
            case "moveSpeed":
                moveSpeedMultiplier += mod.floatValue;
                break;
            case "beetGrowth":
                GlobalConstants.beetGrowthTime -= mod.floatValue;
                if(GlobalConstants.beetGrowthTime < .25f) GlobalConstants.beetGrowthTime = .25f;
                break;
            case "bloodDecay":
                GlobalConstants.bloodDecayModifier -= mod.floatValue;
                if(GlobalConstants.bloodDecayModifier < .1f) GlobalConstants.bloodDecayModifier = .1f;
                break;
            case "pickupRange":
                GlobalConstants.pickupRange += mod.floatValue;
                break;
            case "projRange":
                projectileRange += mod.floatValue;
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

    public override string ToString(){
        return modifies + " " + floatValue + " " + intValue + " " + materialIndex;
    }
}

public static class ModPool{
    public static Mod[] GetMajorUpgrades1(){
        return new Mod[]{
            new Mod("splash", 0, 2.5f),
            new Mod("pierce", 1, 0f, 3),
            new Mod("numProj", 2, 0f, 2)
        };
    }

    public static Mod[] GetRandomUpgrades(){
        Mod[] availableMods = new Mod[]{
            new Mod("attackSpeed", 3, .1f),
            new Mod("beetGrowth", 4, 0.25f),
            new Mod("bloodDecay", 5, 0.1f),
            new Mod("moveSpeed", 6, .1f),
            new Mod("pickupRange", 7, 2.5f),
            new Mod("numProj", 8, 0f, 1),
            new Mod("projRange", 9, 1f)
        };
        Mod[] toReturn = new Mod[3];
        List<int> chosen = new List<int>();
        for(int i = 0; i < 3; i++){
            Mod mod = null;
            while(mod == null){
                int index = Random.Range(0,availableMods.Length);
                if(!chosen.Contains(index)){
                    mod = availableMods[index];
                    chosen.Add(index);
                    toReturn[i] = mod;
                } 
            }
        }
        return toReturn;
    }

    public static Mod[] GetModsForLevel(int level){
        if(level == 5) return GetMajorUpgrades1();
        else return GetRandomUpgrades();
    }

}
