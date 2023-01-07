using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Camera cam;

    float accelerationSpeed = 10f;
    float maxSpeed = 250f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    void FixedUpdate(){
        LookAtMouse();
        HandleKeyInput();
        SyncCameraLocation();
    }

    void LookAtMouse(){
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        var rayStepsToGround = ray.origin.y / Mathf.Abs(ray.direction.y);
        Debug.Log(rayStepsToGround);
        var intersectionPoint = ray.origin + rayStepsToGround * ray.direction;
        intersectionPoint.y = transform.position.y;

        Debug.Log(intersectionPoint);

        transform.LookAt(intersectionPoint);   
    }

    void HandleKeyInput(){
        Rigidbody rb = GetComponent<Rigidbody>();

        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(Vector3.forward * accelerationSpeed);
        } else if(Input.GetKey(KeyCode.S)){
            rb.AddForce(Vector3.back * accelerationSpeed);
        }

        if(Input.GetKey(KeyCode.A)){
            rb.AddForce(Vector3.left * accelerationSpeed);
        } else if(Input.GetKey(KeyCode.D)){
            rb.AddForce(Vector3.right * accelerationSpeed);
        }

        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void SyncCameraLocation(){
        cam.transform.position = new Vector3(transform.position.x, transform.position.y+10f, transform.position.z-10f);
    }
}
