using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSequenceManager : MonoBehaviour
{
    [System.Serializable]
    public struct SequenceInfo {
        public Transform startPos;
        public Transform destPos;
        public Transform lookPos;
        public GameObject disableAfter;
        public float duration;
        public float blankDuration;
    }
    public GameObject blocker;
    private Material blockerMaterial;
    public SlowTranslator cam;
    public List<SequenceInfo> infos = new List<SequenceInfo>();
    public float voDelay = 1f;
    private int currentInfo = -1;
    private float hideTime = 0f;
    private float moveTime = 0f;
    private float startTime = 0f;
    private bool inBlackout = false;
    public GameObject voObject;
    void Update()
    {
        if (startTime == 0f) {
            startTime = Time.time;
            blockerMaterial = blocker.GetComponent<MeshRenderer>().material = Material.Instantiate(blocker.GetComponent<MeshRenderer>().material);
        }
        if (!voObject.activeSelf && Time.time > startTime + voDelay) {
            voObject.SetActive(true);
        }
        if (currentInfo >= infos.Count) {
            return;
        }

        blockerMaterial.color = new Color(0, 0, 0, Mathf.Clamp01((blockerMaterial.color.a + (inBlackout ? 1f : -1f) * Time.deltaTime)));

        if (moveTime <= Time.time) {
            if (currentInfo >= 0 && currentInfo < infos.Count) {
                if (infos[currentInfo].disableAfter) {
                    Debug.Log("Disabling stuff for step " + currentInfo);
                    infos[currentInfo].disableAfter.SetActive(false);
                }
            }

            currentInfo++;
            Debug.Log("Advanced to " + currentInfo);
            if (currentInfo >= infos.Count) {
                Debug.Log("Done with sequence");
                SceneManager.LoadScene("SampleScene");
                return;
            }
            cam.transform.position = infos[currentInfo].startPos.position;
            cam.destination = infos[currentInfo].destPos;
            cam.transform.LookAt(infos[currentInfo].lookPos);
            hideTime = Time.time + infos[currentInfo].duration;
            moveTime = hideTime + infos[currentInfo].blankDuration;
        }

        inBlackout = hideTime <= Time.time;
    }
}
