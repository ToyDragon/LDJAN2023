using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAmountController : MonoBehaviour
{
    // 0 to 1
    public float amount { get; private set; }
    public float deltaPerSecond;

    void FixedUpdate() {
        amount = Mathf.Clamp01(amount + deltaPerSecond * Time.fixedDeltaTime);
    }
    public void Add(float toAdd) {
        amount = Mathf.Clamp01(amount + toAdd);
    }
}