using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodLightController : MonoBehaviour
{
    public Color happyColor = Color.blue;
    public Color angryColor = Color.red;
    public float clipTime = .25f;
    public float clipVolume = .5f;
    private new Light light;
    private float flickerTime;
    private AudioSource audioSource;
    private float lastClipPlayTime = -100f;
    void OnEnable() {
        light = GetComponent<Light>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Time.time - lastClipPlayTime >= clipTime) {
            audioSource.Play();
            lastClipPlayTime = Time.time;
        }
        light.color = Color.Lerp(angryColor, happyColor, BloodAmountController.instance.amount);
        float offsetStrength = 0f;
        if (BloodAmountController.instance.amount < 3f) {
            float missingBlood = 1f - BloodAmountController.instance.amount;
            offsetStrength = Mathf.Clamp01((missingBlood - .5f) * 2f);
        }
        audioSource.volume = clipVolume * offsetStrength;
        flickerTime += offsetStrength * Time.deltaTime * 10f;
        light.intensity = 5000 + Mathf.Sin(flickerTime) * offsetStrength * 5000;
    }
}
