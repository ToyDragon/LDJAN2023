using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMods: MonoBehaviour
{
    public int numberOfProjectiles = 1;
    public bool piercing = false;
    public int numberOfPierces = 0;
    public bool splashDamage = false;
    public float splashDamageRadius = 2.5f;
    public float splashDamageModifier = 0.25f;
    public float attackCooldownFast = 0.25f;
    public float attackCooldownSlow = .5f;
    public float attackSpeedMultiplier = 1.0f;
    public float moveSpeedMultiplier = 1.0f;
    public float projectileRange = 2.5f;
    public float damage = 1.0f;
    public float damageMulti = 1.0f;

    public void Start(){
        StatManager.attackMods = this;
    }

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
                piercing = true;
                numberOfPierces += mod.intValue;
                break;
            case "splash":
                splashDamage = true;
                splashDamageRadius = 5f;
                break;
            case "splashRadius":
                splashDamage = true;
                splashDamageRadius += mod.floatValue;
                break;
            case "splashMod":
                splashDamage = true;
                splashDamageModifier += mod.floatValue;
                break;
            case "attackSpeed":
                attackSpeedMultiplier -= mod.floatValue;
                if(attackSpeedMultiplier < .1f) attackSpeedMultiplier = .1f;
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
            case "attackDamage":
                damage += mod.floatValue;
                break;
            case "attackDamageMulti":
                damageMulti += mod.floatValue;
                break;
        }
    }
}

public class Mod{
    public string modifies;
    public float floatValue;
    public int intValue;
    public int materialIndex;
    public string disp;
    public int imageIndex;

    public Mod(string modifies, int matIndex, float fv = 0f, int iv = 0, string d = "", int img = 0){
        this.modifies = modifies;
        floatValue = fv;
        intValue = iv;
        materialIndex = matIndex;
        disp = d;
        imageIndex = img;
    }

    public override string ToString(){
        return modifies + " " + floatValue + " " + intValue + " " + materialIndex;
    }
}

public static class ModPool{
    public static Mod[] GetMajorUpgrades1(){
        return new Mod[]{
            new Mod("splash", 0, 2.5f, 0, "Bomb Shot", 1),
            new Mod("pierce", 1, 0f, 3, "Piercing Shot", 2),
            new Mod("numProj", 2, 0f, 2, "Extra Arrows", 3)
        };
    }

    public static Mod[] GetRandomUpgrades(int level){
        Mod[] availableMods = new Mod[]{
            new Mod("attackSpeed", 3, .1f, 0, "Faster Attack Speed"),
            new Mod("beetGrowth", 4, 0.25f, 0, "Faster Beet Growth"),
            new Mod("bloodDecay", 5, 0.1f, 0, "Slower Blood Decay"),
            new Mod("moveSpeed", 6, .1f, 0, "Faster Movement"),//
            new Mod("pickupRange", 7, 2.5f, 0, "Farther Pickup Range"),
            new Mod("projRange", 9, 1f, 0, "Farther Arrow Range"),
            new Mod("attackDamage", 11, 0.5f, 0, "Added base damage"),
            new Mod("attackDamageMulti", 10, 0.1f, 0, "More multiplicitave damage")
        };

        Mod[] availableAfterFirstMajor = new Mod[]{
            new Mod("numProj", 8, 0f, 1, "Extra Arrows"),
            new Mod("pierce", 12, 0f, 1, "Arrows pierce more enemies"),
            new Mod("splashMod", 13, 0.25f, 0, "Explosions do more damage"),
            new Mod("splashRadius", 14, 1f, 0, "Explosions are bigger")
        };

        List<Mod> mods = new List<Mod>();
        mods.AddRange(availableMods);
        if(level > 5) mods.AddRange(availableAfterFirstMajor);
        Mod[] toReturn = new Mod[3];
        List<int> chosen = new List<int>();
        for(int i = 0; i < 3; i++){
            Mod mod = null;
            while(mod == null){
                int index = Random.Range(0,mods.Count);
                if(!chosen.Contains(index)){
                    mod = mods[index];
                    chosen.Add(index);
                    toReturn[i] = mod;
                } 
            }
        }
        return toReturn;
    }

    public static Mod[] GetModsForLevel(int level){
        if(level == 5) return GetMajorUpgrades1();
        else return GetRandomUpgrades(level);
    }

}
