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
            beetController.health = DifficultyOverTimeManager.GetDifficultyMultiplier();
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
