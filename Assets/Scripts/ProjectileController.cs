using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;
    public float despawnTime = 5.0f;
    private float aliveTime = 0f;
    public bool pierce = false;
    public int maxPierces = 0;
    private int numPierced = 0;
    public bool splash = false;
    public float splashRadius = 0f;
    public float splashDamageMultiplier = 0f;
    public GameObject explosionPrefab;
    private List<BloodBeetController> beetsPierced = new List<BloodBeetController>();
    private bool destroying = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.SuppressUpdates()) return;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        aliveTime += Time.deltaTime;
        if(aliveTime > despawnTime) GameObject.Destroy(gameObject);
    }

    void fixedUpdate(){

    }

    void OnTriggerEnter(Collider collider){
        if(destroying || GameState.selectingUpgrade) return;
        //Debug.Log("Collided with " + collider.gameObject.name + " at position " + collider.transform.position);
        BloodBeetController beet = collider.gameObject.GetComponent<BloodBeetController>();
        if(beet != null){
            if(pierce){
                if(beetsPierced.Contains(beet)) return;
                //Debug.Log("projectile is piercing, and hasn't hit this beet yet");
                beet.HandleHit(new HitData());
                beetsPierced.Add(beet);
                numPierced++;
                if(numPierced >= maxPierces){
                    //Debug.Log("max pierces reached, destroying");
                    destroying = true;
                    GameObject.Destroy(gameObject);
                } 
            } else {
                if(splash){
                    //Debug.Log("Doing splash damage for projectile " + gameObject.name);
                    GameObject explosion = GameObject.Instantiate(explosionPrefab);
                    explosion.GetComponent<ExplosionHandler>().maxSize = splashRadius;
                    explosion.transform.position = transform.position;
                    Collider[] explosionHits = Physics.OverlapSphere(transform.position, splashRadius);
                    List<BloodBeetController> hitByExplosion = new List<BloodBeetController>();
                    foreach(var hit in explosionHits){
                        BloodBeetController beetHit = hit.GetComponent<BloodBeetController>();
                        if(beetHit != null && !hitByExplosion.Contains(beetHit)){
                            hitByExplosion.Add(beetHit);
                            //Debug.Log("hit a beet with explosion!");
                            beetHit.HandleHit(new HitData());
                        }
                    }
                }
                beet.HandleHit(new HitData());
                GameObject.Destroy(gameObject);
                destroying = true;
            }
        }
    }

    public void SetParameters(AttackMods mods){
        pierce = mods.piercing;
        maxPierces = mods.numberOfPierces;
        splash = mods.splashDamage;
        splashRadius = mods.splashDamageRadius;
        splashDamageMultiplier = mods.splashDamageModifier;
        despawnTime = mods.projectileRange;
    }
}
