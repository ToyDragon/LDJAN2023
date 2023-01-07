using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTranslator : MonoBehaviour
{
    public Transform destination;
    public Vector3 direction;
    public float speed = .1f;
    void Update()
    {
        if (destination) {
            direction = destination.position - transform.position;
        }
        transform.position += direction.normalized * speed * Time.deltaTime;
    }
}
