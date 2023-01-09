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
        if(GameState.SuppressUpdates()) return;
        timeSinceStart += Time.deltaTime;
        int seconds = (int)timeSinceStart;
        int minutes = seconds/60;
        int leftover = seconds%60;

        string secondsS = leftover == 0 ? "00" :
            leftover < 10 ? "0" + leftover :
                "" + leftover;

        GetComponent<TMPro.TMP_Text>().text = minutes+":"+secondsS;
    }
}
