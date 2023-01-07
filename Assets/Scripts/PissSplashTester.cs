using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PissSplashTester : MonoBehaviour
{
    public Material pissMat;
    void OnEnable() {
        pissMat = GetComponent<MeshRenderer>().sharedMaterial;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            pissMat.SetFloat("_StartTime", Time.time + .25f);
        }
    }
}
