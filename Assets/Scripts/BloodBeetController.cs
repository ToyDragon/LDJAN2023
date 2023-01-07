using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBeetController : MonoBehaviour
{
    public BeetState state = BeetState.IDLE;
    int health = 3;
    float timeToGrow = 2.0f;
    float growthProgress = 0f;
    public GameObject pickupPrefab;

    public void HandleHit(HitData hitData){
        if(state == BeetState.IDLE){
            state = BeetState.GROWING;
        } else if(state == BeetState.ALIVE){
            health -= hitData.damage;
            if(health <= 0){
                HandleDeath();
            }
        }
    }

    void Update(){
        if(state == BeetState.GROWING){
            growthProgress += Time.deltaTime;
            if(growthProgress >= timeToGrow) state = BeetState.ALIVE;
        }
    }

    void HandleDeath(){
        GameObject pickup = GameObject.Instantiate(pickupPrefab);
        pickup.transform.position = transform.position;
        GameObject.Destroy(gameObject);
    }
}

public enum BeetState{
    IDLE, GROWING, ALIVE
}