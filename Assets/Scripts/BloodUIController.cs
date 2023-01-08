using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodUIController : MonoBehaviour
{
    public BloodAmountController bloodController;
    public float decayFactor = .99f;
    public float closeDecayFactor = .95f;
    public float maxBloodVel = 1f;
    public float acceleration = .15f;
    public RectTransform displayTransform;
    private float bloodVelocity = 0f;
    void FixedUpdate() {
        bloodVelocity *= decayFactor;
        float height = ((RectTransform)displayTransform.parent).rect.yMax - ((RectTransform)displayTransform.parent).rect.yMin;
        float displayedPercent = (displayTransform.sizeDelta.y / height);
        float bloodAmount = bloodController.amount;
        bloodVelocity += (bloodAmount - displayedPercent) * acceleration;
        if (Mathf.Abs(bloodAmount - displayedPercent) < .1) {
            bloodVelocity *= closeDecayFactor;
        }
        bloodVelocity = Mathf.Clamp(bloodVelocity, -maxBloodVel, maxBloodVel);
    }
    void Update()
    {
        float height = ((RectTransform)displayTransform.parent).rect.yMax - ((RectTransform)displayTransform.parent).rect.yMin;
        float displayedPercent = (displayTransform.sizeDelta.y / height);
        float newPercent = Mathf.Clamp01(displayedPercent + bloodVelocity * Time.deltaTime);
        Debug.Log("From " + displayedPercent + " to " + newPercent);
        displayTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, newPercent * height);
    }
}
