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
        GameObject pickup = GameObject.Instantiate(pickupPrefab);
        pickup.transform.position = transform.position;
        GameObject.Destroy(gameObject);
    }
}

public enum BeetState{
    IDLE, GROWING, ALIVE
}