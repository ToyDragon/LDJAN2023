using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Camera cam;

    public float maxVelocity = 5f;
    public float acceleration = 0.5f;
    public float deccelerate = 1f;
    public float xVel, zVel;

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
            zVel += acceleration;
            if(zVel < 0f) zVel += deccelerate;
        } else if(Input.GetKey(KeyCode.S)){
            zVel -= acceleration;
            if(zVel > 0f) zVel -= deccelerate;
        } else if(zVel != 0f){
            bool negative = zVel < 0;
            if(negative){
                zVel = Mathf.Clamp(zVel + deccelerate, zVel, 0f);
            } else {
                zVel = Mathf.Clamp(zVel - deccelerate, 0f, zVel);
            }
        }

        if(Input.GetKey(KeyCode.A)){
            xVel -= acceleration;
            if(xVel > 0f) xVel -= deccelerate;
        } else if(Input.GetKey(KeyCode.D)){
            xVel += acceleration;
            if(xVel < 0f) xVel += deccelerate;
        } else if(xVel != 0f){
            bool negative = xVel < 0;
            if(negative){
                xVel = Mathf.Clamp(xVel + deccelerate, xVel, 0f);
            } else {
                xVel = Mathf.Clamp(xVel - deccelerate, 0f, xVel);
            }
        }

        xVel = Mathf.Clamp(xVel, -maxVelocity, maxVelocity);
        zVel = Mathf.Clamp(zVel, -maxVelocity, maxVelocity);

        Vector2 velocity = new Vector2(xVel, zVel);
        if(velocity.magnitude > maxVelocity){
            velocity = velocity.normalized * maxVelocity;
        }

        transform.Translate(new Vector3(velocity.x * 0.1f, 0f, velocity.y * 0.1f), Space.World);
    }

    void SyncCameraLocation(){
        cam.transform.position = new Vector3(transform.position.x, transform.position.y+20f, transform.position.z-20f);
    }
}
