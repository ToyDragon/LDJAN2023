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
        if (Input.GetKeyDown(KeyCode.F2)) {
            Debug.Log("CHEAT spawn boss");
            BossManager.instance.lastBossSpawn = Time.time - BossManager.instance.gapBetweenBosses;
        }
        if (Input.GetKeyDown(KeyCode.F3)) {
            AttackMods vampMods = GameObject.Find("Vampire").GetComponent<AttackMods>();
            XPLevelController.instance.AddXP(XPLevelController.instance.requiredXP * 10f);
            for (int i = 0; i < 5; i++) {
                Mod[] mods = ModPool.GetModsForLevel(XPLevelController.instance.level);
                foreach (var mod in mods) {
                    vampMods.ApplyMod(mod);
                }
            }
            Debug.Log("CHEAT bunch of upgrades");
        }
        if (Input.GetKeyDown(KeyCode.F5)) {
            Debug.Log("CHEAT kill yourself");
            BloodAmountController.instance.Add(-BloodAmountController.instance.amount);
        }
    }
    #endif
}
