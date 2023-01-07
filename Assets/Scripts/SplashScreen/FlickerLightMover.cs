using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLightMover : MonoBehaviour
{
    public float delayBase = .5f;
    public float delayRange = .3f;
    public float flickerMagnitude = .3f;
    private float nextFlicker = 0f;
    private Vector3 targetPos;
    private Vector3 startingPos;
    void OnEnable()
    {
        startingPos = transform.position;
        targetPos = transform.position;
    }
    void OnDisable() {
        transform.position = startingPos;
    }

    void Update()
    {
        var toTarget = (targetPos - transform.position);
        transform.position += toTarget * 3f * Time.deltaTime;
        if (Time.time <= nextFlicker) {
            return;
        }
        nextFlicker = Time.time + delayBase + Random.Range(-delayRange, delayRange);
        targetPos = startingPos + Random.onUnitSphere * Random.Range(0, flickerMagnitude);
    }
}
