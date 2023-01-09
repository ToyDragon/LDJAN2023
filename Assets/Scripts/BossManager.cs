using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;
    public GameObject arenaPrefab;
    public List<GameObject> bossPrefabs = new List<GameObject>();
    [HideInInspector]
    public float lastBossSpawn = 0;
    public float gapBetweenBosses = 60;
    public float acceleration = 0.8f;
    private GameObject vampire;
    private GameObject arena;
    private AudioSource audioSource;
    private BloodBeetController[] livingBosses;
    void OnEnable() {
        vampire = GameObject.Find("Vampire");
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }
    void Update()
    {
        if (livingBosses != null) {
            foreach (var boss in livingBosses) {
                if (boss) {
                    return; // Skip if there are still living bosses
                }
            }
            livingBosses = null;
            if (arena) {
                Destroy(arena);
                arena = null;
            }
        }

        if (livingBosses == null && Time.time - lastBossSpawn > gapBetweenBosses) {
            gapBetweenBosses *= acceleration;
            gapBetweenBosses = Mathf.Max(1, gapBetweenBosses);
            lastBossSpawn = Time.time;

            arena = GameObject.Instantiate(arenaPrefab);
            arena.transform.position = vampire.transform.position - Vector3.up * vampire.transform.position.y;

            var newBoss = GameObject.Instantiate(bossPrefabs[Random.Range(0, bossPrefabs.Count)]);
            newBoss.transform.position = vampire.transform.position + vampire.transform.forward * 30f;
            livingBosses = newBoss.GetComponentsInChildren<BloodBeetController>();
            foreach (var beetController in livingBosses) {
                beetController.HandleHit(new HitData() {damage = 0}); // trigger growth
            }
            newBoss.transform.LookAt(vampire.transform.position);
            audioSource.Play();
        }
    }
}
