using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleScroller : MonoBehaviour
{
    public float duration = 26f;
    public float totalDistance = 2000f;
    private float startTime;
    private Vector3 startingOffsetFromParent;
    void Update()
    {
        if (startTime == 0) {
            startTime = Time.time;
            startingOffsetFromParent = transform.position - transform.parent.position;
        }
        float progress = Mathf.Clamp01((Time.time - startTime) / duration);
        transform.position = transform.parent.position + startingOffsetFromParent + new Vector3(0, progress * totalDistance, 0);
    }
}
