using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    float timeSinceStart = 0f;
    float maxTime = 0.25f;
    public float maxSize = 5f;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("created explosion effect");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.SuppressUpdates()) return;
        timeSinceStart += Time.deltaTime;
        transform.GetChild(0).transform.localScale = new Vector3(1f,1f,1f) * (timeSinceStart/maxTime) * maxSize;
        if(timeSinceStart > maxTime) GameObject.Destroy(gameObject);
    }
}
