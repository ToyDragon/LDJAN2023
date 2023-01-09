using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAmountController : MonoBehaviour
{
    public GameObject youDied;
    public static BloodAmountController instance;
    // 0 to 1
    public float amount { get; private set; }
    public float deltaPerSecond;
    public bool isDead;
    void OnEnable() {
        instance = this;
        amount = 1f;
    }
    void FixedUpdate() {
        if(GameState.SuppressUpdates()) return;
        amount = Mathf.Clamp01(amount + deltaPerSecond * (DifficultyOverTimeManager.GetDifficultyMultiplier() / 5f) * GlobalConstants.bloodDecayModifier * Time.fixedDeltaTime);
        if(!isDead && amount == 0 && PickupController.amtInFlight == 0){
            isDead = true;
            youDied.SetActive(true);
        }
    }
    public float Add(float toAdd) {
        float actualAdded = Mathf.Min(1f - amount, toAdd);
        amount += actualAdded;
        return toAdd - actualAdded;
    }
}
