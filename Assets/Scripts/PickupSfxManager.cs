using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSfxManager : MonoBehaviour
{
    public static PickupSfxManager instance;
    public int maxQueueSize = 3;
    public float soundDelay = 1f;
    private AudioSource audioSource;

    public AudioClip pickupSound;
    private int queuedPickups = 0;
    private float lastPickup = -100;
    
    public List<AudioClip> finishSounds = new List<AudioClip>();
    private float queuedFinishes = 0;
    private float lastFinish = -100;
    private int finishSoundScore = 0;
    private float lastFinishScoreDecay = -100;
    void OnEnable() {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (queuedFinishes > 0 && Time.time - lastFinish >= soundDelay) {
            queuedFinishes--;
            lastFinish = Time.time;
            audioSource.PlayOneShot(finishSounds[finishSoundScore]);
            finishSoundScore = finishSoundScore + 1;
            if (finishSoundScore >= finishSounds.Count) {
                finishSoundScore -= 4;
            }
        }

        if (queuedPickups > 0 && Time.time - lastPickup >= soundDelay) {
            queuedPickups--;
            lastPickup = Time.time;
            audioSource.PlayOneShot(pickupSound);
        }

        float timeSinceDecayOrPlay = Mathf.Min(Time.time - lastFinish, Time.time - lastFinishScoreDecay);
        if (finishSoundScore > 0 && timeSinceDecayOrPlay > soundDelay * 5f) {
            lastFinishScoreDecay = Time.time;
            finishSoundScore--;
        }
    }
    public void PickupStartedInstance() {
        queuedPickups = Mathf.Min(queuedPickups + 1, maxQueueSize);
    }
    public void PickupFinishedInstance() {
        queuedFinishes = Mathf.Min(queuedFinishes + 1, maxQueueSize);
    }
    public static void PickupStarted() {
        instance.PickupStartedInstance();
    }
    public static void PickupFinished() {
        instance.PickupFinishedInstance();
    }
}
