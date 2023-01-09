using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    float timeSinceStart = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        int seconds = (int)timeSinceStart;
        int minutes = seconds/60;
        int leftover = seconds%60;

        GetComponent<TMPro.TMP_Text>().text = minutes+":"+leftover;
    }
}
