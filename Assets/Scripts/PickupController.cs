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
    // Start is called before the first frame update
    void Start()
    {
        vampire = GameObject.Find("Vampire");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(vampire.transform.position, transform.position);
        if(!following){
            if(dist <= pickupRange) following = true;
        } else {
            if(dist <= collectRange) HandleCollect();
            transform.LookAt(vampire.transform.position);
            transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
        }
    }

    void HandleCollect(){
        var bloodAmountController = vampire.GetComponent<BloodAmountController>();
        if (bloodAmountController) {
            bloodAmountController.Add(.4f);
        }
        GameObject.Destroy(gameObject);
    }
}

