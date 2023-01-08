using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAmountController : MonoBehaviour
{
    public static BloodAmountController instance;
    // 0 to 1
    public float amount { get; private set; }
    public float deltaPerSecond;
    void OnEnable() {
        instance = this;
        amount = 1f;
    }
    void FixedUpdate() {
        amount = Mathf.Clamp01(amount + deltaPerSecond * Time.fixedDeltaTime);
    }
    public float Add(float toAdd) {
        float actualAdded = Mathf.Min(1f - amount, toAdd);
        amount += actualAdded;
        return toAdd - actualAdded;
    }
}
