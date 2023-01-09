using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    float timeSinceStart = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable(){
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.SuppressUpdates() || BloodAmountController.instance.isDead) return;
        timeSinceStart += Time.deltaTime;
        
        GetComponent<TMPro.TMP_Text>().text = GetTimeString();
    }

    public string GetTimeString(){
        int seconds = (int)timeSinceStart;
        int minutes = seconds/60;
        int leftover = seconds%60;

        string secondsS = leftover == 0 ? "00" :
            leftover < 10 ? "0" + leftover :
                "" + leftover;

        return minutes+":"+secondsS;
    }
}
