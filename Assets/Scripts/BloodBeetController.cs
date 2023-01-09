using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBeetController : MonoBehaviour
{
    public BeetState state = BeetState.IDLE;
    public float health = 1f;
    //float timeToGrow = 2.0f;
    float growthProgress = 0f;
    public GameObject pickupPrefab;
    private float heightChangeForGrow = 0;
    private Transform modelOffset;
    public int difficulty = 0;
    float timeToGrow;
    void OnEnable() {
        modelOffset = transform.Find("ModelOffset");
    }

    public void HandleHit(HitData hitData){
        if(state == BeetState.IDLE){
            timeToGrow = GlobalConstants.beetGrowthTime;
            state = BeetState.GROWING;
            heightChangeForGrow = -modelOffset.localPosition.y;
        } else if(state == BeetState.ALIVE){
            //Debug.Log("lowering health by " + hitData.damage + "\nNew health is " + (health-hitData.damage));
            health -= hitData.damage;
            if(health <= 0){
                HandleDeath();
            }
        }
    }
    public void SetDifficulty(int difficulty) {
        this.difficulty = difficulty;
        UpdateDifficultyDisplay();
    }
    void UpdateDifficultyDisplay() {
        var partsEasy = GetComponentsInChildren<ShowDifficultyEasy>();
        var partsMedium = GetComponentsInChildren<ShowDifficultyMedium>();
        var partsHard = GetComponentsInChildren<ShowDifficultyHard>();
        foreach (var part in partsEasy) {
            part.gameObject.SetActive(difficulty == 0);
        }
        foreach (var part in partsMedium) {
            part.gameObject.SetActive(difficulty == 1);
        }
        foreach (var part in partsHard) {
            part.gameObject.SetActive(difficulty == 2);
        }
    }

    void Update(){
        if(GameState.SuppressUpdates()) return;
        if(state == BeetState.GROWING){
            growthProgress += Time.deltaTime;
            modelOffset.localPosition += Vector3.up * heightChangeForGrow * Time.deltaTime / timeToGrow;
            if (growthProgress >= timeToGrow) {
                state = BeetState.ALIVE;
                modelOffset.localPosition = Vector3.zero;
            }
        }
    }

    void HandleDeath(){
        StatManager.numberOfBeetsKilled++;
        if(difficulty == 3) StatManager.numberOfBossesKilled++;
        GameObject pickup = GameObject.Instantiate(pickupPrefab);
        pickup.transform.position = transform.position;
        var pickupControllers = pickup.GetComponentsInChildren<PickupController>();
        foreach (var pickupController in pickupControllers) {
            if (difficulty == 1) {
                pickupController.XP *= 10;
            }
            if (difficulty == 2) {
                pickupController.XP *= 100;
            }
            if (difficulty == 3) {
                pickupController.XP *= 500;
            }
        }

        GameObject.Destroy(gameObject);
    }
}

public enum BeetState{
    IDLE, GROWING, ALIVE
}