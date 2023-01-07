using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    float speed = 10f;
    float despawnTime = 5.0f;
    float aliveTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        aliveTime += Time.deltaTime;
        if(aliveTime > despawnTime) GameObject.Destroy(gameObject);
    }

    void fixedUpdate(){

    }

    void OnTriggerEnter(Collider collider){
        Debug.Log("Collided with " + collider.gameObject.name);
        BloodBeetController beet = collider.gameObject.GetComponent<BloodBeetController>();
        if(beet != null){
            beet.HandleHit(new HitData());
            GameObject.Destroy(gameObject);
        }
    }
}
