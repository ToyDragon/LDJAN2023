using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxVelocity = 5f;
    public float acceleration = 0.5f;
    public float deccelerate = 1f;
    public float slowMoveSpeed = 2f;
    public float fastMoveSpeed = 5f;
    public float xVel, zVel;
    public Vector3 offsetToCamera;
    private UnityEngine.CharacterController unityCharController;
    public GameObject projectilePrefab;
    public float slowAttackCooldown = 1f;
    public float fastAttackCooldown = .1f;
    private float timeSinceLastAttack = 0f;
    private GameObject legRoot;
    private Vector3 lastVelocityThree;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip attackSound;
    public BloodAmountController bloodAmountController;
    public Vector3 projectileSpread = new Vector3(0f, 2.5f, 0f);
    void Start()
    {
        unityCharController = GetComponent<UnityEngine.CharacterController>();
        if (offsetToCamera == Vector3.zero) {
            offsetToCamera = Camera.main.transform.position - transform.position;
        }
        animator = GetComponent<Animator>();
        legRoot = transform.Find("Torso").Find("Lower Body").gameObject;
        audioSource = GetComponent<AudioSource>();
        bloodAmountController = GetComponent<BloodAmountController>();
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
        legRoot.transform.LookAt(legRoot.transform.position + Quaternion.Euler(0, -90, 0) * lastVelocityThree);
    }

    void HandleKeyInput(){
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

        var velocityThree = new Vector3(velocity.x, 0, velocity.y);
        if (velocityThree.magnitude > .2f) {
            lastVelocityThree = velocityThree;
        }

        float moveSpeed = Mathf.Lerp(slowMoveSpeed, fastMoveSpeed, bloodAmountController.amount);
        unityCharController.SimpleMove(velocityThree * moveSpeed);
        animator.SetFloat("speed", velocityThree.magnitude * moveSpeed);
    }

    void HandleMouseInput(){
        timeSinceLastAttack += Time.deltaTime;
        float attackCooldown = Mathf.Lerp(slowAttackCooldown, fastAttackCooldown, bloodAmountController.amount);
        if(Input.GetMouseButton(0) && timeSinceLastAttack >= attackCooldown){
            Attack();
        }
    }

    void Attack(){
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var rayStepsToGround = ray.origin.y / Mathf.Abs(ray.direction.y);
        var intersectionPoint = ray.origin + rayStepsToGround * ray.direction;
        intersectionPoint.y = transform.position.y;
        AttackMods mods = GetComponent<AttackMods>();
        
        for(int i = 0; i < mods.numberOfProjectiles; i++){
            int offsetAmount = i - (mods.numberOfProjectiles/2); 
            GameObject projectile = GameObject.Instantiate(projectilePrefab);

            ProjectileController controller = projectile.GetComponent<ProjectileController>();
            controller.SetParameters(mods);

            projectile.transform.position = transform.position;
            Vector3 toLookAt = intersectionPoint;
            
            projectile.transform.LookAt(toLookAt);
            projectile.transform.Rotate(offsetAmount * projectileSpread);
        }
        
        
        timeSinceLastAttack = 0f;

        audioSource.PlayOneShot(attackSound);
    }

    void SyncCameraLocation(){
        Camera.main.transform.position = transform.position + offsetToCamera;
    }
}
