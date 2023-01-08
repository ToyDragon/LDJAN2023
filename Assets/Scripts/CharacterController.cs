using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxVelocity = 5f;
    public float acceleration = 0.5f;
    public float deccelerate = 1f;
    public float moveSpeed = 10f;
    public float xVel, zVel;
    public Vector3 offsetToCamera;
    private UnityEngine.CharacterController unityCharController;
    public GameObject projectilePrefab;
    public float attackCooldown = 1f;
    private float timeSinceLastAttack = 0f;

    void Start()
    {
        unityCharController = GetComponent<UnityEngine.CharacterController>();
        if (offsetToCamera == Vector3.zero) {
            offsetToCamera = Camera.main.transform.position - transform.position;
        }
    }

    void Update(){
        HandleMouseInput();
    }

    void FixedUpdate(){
        LookAtMouse();
        HandleKeyInput();
        SyncCameraLocation();
    }

    void LookAtMouse(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var rayStepsToGround = ray.origin.y / Mathf.Abs(ray.direction.y);
        var intersectionPoint = ray.origin + rayStepsToGround * ray.direction;
        intersectionPoint.y = transform.position.y;
        transform.LookAt(intersectionPoint);   
    }

    void HandleKeyInput(){
        if(Input.GetKeyDown(KeyCode.Backspace)){
            GameState.selectingUpgrade = !GameState.selectingUpgrade;
            Camera.main.GetComponent<UIController>().ShowCards(GameState.selectingUpgrade);
        }

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

        unityCharController.SimpleMove(new Vector3(velocity.x * moveSpeed, 0f, velocity.y * moveSpeed));
        // transform.Translate(, Space.World);
    }

    void HandleMouseInput(){
        timeSinceLastAttack += Time.deltaTime;
        if(Input.GetMouseButton(0) && timeSinceLastAttack >= attackCooldown){
            Attack();
        }
    }

    void Attack(){
        GameObject projectile = GameObject.Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var rayStepsToGround = ray.origin.y / Mathf.Abs(ray.direction.y);
        var intersectionPoint = ray.origin + rayStepsToGround * ray.direction;
        intersectionPoint.y = transform.position.y;
        projectile.transform.LookAt(intersectionPoint);
        timeSinceLastAttack = 0f;
    }

    void SyncCameraLocation(){
        Camera.main.transform.position = transform.position + offsetToCamera; // new Vector3(transform.position.x, transform.position.y+20f, transform.position.z-20f);
    }
}
