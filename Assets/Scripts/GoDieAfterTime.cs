using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDieAfterTime : MonoBehaviour
{
    public float dieTime;
    void Update()
    {
        if (Time.time > dieTime) {
            Destroy(gameObject);
        }
    }
}
