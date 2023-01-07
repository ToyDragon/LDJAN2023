using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.position = target.transform.position;
    }
}
