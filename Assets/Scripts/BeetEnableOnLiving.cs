using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetEnableOnLiving : MonoBehaviour
{
    public List<GameObject> objsToEnableOnLiving = new List<GameObject>();
    public List<MonoBehaviour> behaviorsToEnableOnLiving = new List<MonoBehaviour>();
    private BloodBeetController beetController;
    private bool shown = false;
    void Start() {
        beetController = GetComponent<BloodBeetController>();
        foreach (var obj in objsToEnableOnLiving) {
            obj.SetActive(false);
        }
        foreach (var behavior in behaviorsToEnableOnLiving) {
            behavior.enabled = false;
        }
    }
    void Update()
    {
        if (!shown && beetController.state == BeetState.ALIVE) {
            switch(beetController.difficulty){
                case 0:
                    beetController.health = 1;
                    break;
                case 1:
                    beetController.health = 8;
                    break;
                case 2:
                    beetController.health = 20;
                    break;
                case 3:
                    beetController.health = 100;
                    break;
            }
            beetController.health *= DifficultyOverTimeManager.GetDifficultyMultiplier();
            foreach (var obj in objsToEnableOnLiving) {
                obj.SetActive(true);
            }
            foreach (var behavior in behaviorsToEnableOnLiving) {
                behavior.enabled = true;
            }
            shown = true;
        }
    }
}
