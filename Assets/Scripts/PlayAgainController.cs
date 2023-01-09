using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            GameState.dead = false;
            GameState.paused = false;
            GameState.selectingUpgrade = false;
            StatManager.numberOfBossesKilled = 0;
            StatManager.numberOfBeetsKilled = 0;
        }
    }
}
