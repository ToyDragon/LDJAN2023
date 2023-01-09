using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetSfxManager : MonoBehaviour
{
    public List<AudioClip> jumpSounds = new List<AudioClip>();
    public static BeetSfxManager instance;
    public float lastJumpSound;
    void OnEnable() {
        instance = this;
    }
    public static bool CanPlayJumpSound() {
        if (Time.time - instance.lastJumpSound > .25f) {
            instance.lastJumpSound = Time.time;
            return true;
        }
        return false;
    }
    public static AudioClip JumpSound() {
        return instance.jumpSounds[Random.Range(0, instance.jumpSounds.Count)];
    }
}
