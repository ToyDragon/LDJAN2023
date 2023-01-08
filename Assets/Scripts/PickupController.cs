using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    GameObject vampire;
    bool following = false;
    float pickupRange = 5f;
    float collectRange = 1f;
    float followSpeed = 25f;
    public bool goToBloodBar;
    // private float barHeight;
    public GameObject uiObjectPrefab;
    private BloodAmountController bloodAmountController;
    private GameObject uiObject;
    private float timeUIUpExpires = -100f;
    private float uiObjUpVel = 1000f;
    private float uiObjSideVel = 0f;
    // Start is called before the first frame update
    void Start()
    {
        vampire = GameObject.Find("Vampire");
        if (goToBloodBar) {
            bloodAmountController = vampire.GetComponent<BloodAmountController>();
            // var bloodBar = GameObject.Find("Blood bar");
            // barHeight = ((RectTransform)bloodBar.transform).rect.yMax - ((RectTransform)bloodBar.transform).rect.yMin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float dist = Vector3.Distance(vampire.transform.position, transform.position);
        if(!following){
            if(dist <= pickupRange) {
                PickupSfxManager.PickupStarted();
                following = true;
                if (goToBloodBar) {
                    uiObject = GameObject.Instantiate(uiObjectPrefab);
                    uiObject.transform.SetParent(GameObject.Find("Canvas").transform);
                    uiObject.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                    foreach (var renderer in GetComponentsInChildren<Renderer>()) {
                        renderer.enabled = false;
                    }
                    timeUIUpExpires = Time.time + .3f;
                }
            }
        } else {
            if (uiObject) {
                uiObjUpVel *= .995f;
                uiObjSideVel += Time.deltaTime * 2000f;
                var target = BloodUIController.instance.GetBarTopPosition();
                var toTarget = target - uiObject.transform.position;
                var toTargetScaled = toTarget.normalized * uiObjSideVel;
                uiObject.transform.position += (toTargetScaled + Vector3.up * uiObjUpVel) * Time.deltaTime;
                if (toTarget.magnitude <= 20f) {
                    HandleCollect();
                }
            } else {
                if(dist <= collectRange) HandleCollect();
                transform.LookAt(vampire.transform.position);
                transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
            }
        }
    }

    void HandleCollect(){
        PickupSfxManager.PickupFinished();
        var bloodAmountController = vampire.GetComponent<BloodAmountController>();
        if (bloodAmountController) {
            bloodAmountController.Add(.4f);
        }
        if (uiObject) {
            GameObject.Destroy(uiObject);
        }
        GameObject.Destroy(gameObject);
    }
}

