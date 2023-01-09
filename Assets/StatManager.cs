using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StatManager{
    public static int numberOfBeetsKilled = 0;
    public static int numberOfBossesKilled = 0;

    public static AttackMods attackMods;

    public static string GenerateStatText(){
        string toRet = "";
        toRet += "Time survived - " + TimerController.instance.GetTimeString();
        toRet += "\nNumber of beets killed - " + numberOfBeetsKilled;
        toRet += "\nNumber of bosses killed - " + numberOfBossesKilled;
        toRet += "\nAttack damage - " + attackMods.damage;
        toRet += "\nAttack damage multiplier - " + attackMods.damageMulti;
        toRet += "\nAttack speed - " + attackMods.attackSpeedMultiplier;
        toRet += "\nNumber of pierces - " + attackMods.numberOfPierces;
        toRet += "\nNumber of projectiles - " + attackMods.numberOfProjectiles;
        toRet += "\nSplash radius - " + attackMods.splashDamageRadius;
        toRet += "\nSplash damage modifier - " + attackMods.splashDamageModifier;
        toRet += "\nMovement speed - " + attackMods.moveSpeedMultiplier;
        toRet += "\nBlood drain delay modifier - " + GlobalConstants.bloodDecayModifier;
        toRet += "\nBeet growth time - " + GlobalConstants.beetGrowthTime;
        toRet += "\nPickup range - " + GlobalConstants.pickupRange;
        return toRet;
    }
}
