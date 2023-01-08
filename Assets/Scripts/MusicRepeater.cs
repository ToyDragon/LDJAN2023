using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRepeater : MonoBehaviour
{
    public float musicLength;
    private float lastPlaytime;
    private AudioSource audioSource;
    void OnEnable() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        lastPlaytime = Time.time;
    }
    void Update()
    {
        if (musicLength <= 0) {
            return;
        }
        if (Time.time - lastPlaytime >= musicLength) {
            audioSource.Play();
            lastPlaytime = Time.time;
        }
    }
}
