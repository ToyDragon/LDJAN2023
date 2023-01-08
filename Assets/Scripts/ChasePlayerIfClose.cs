using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerIfClose : MonoBehaviour
{
    public Transform modelOffset;
    public float activateDistance = 20f;
    public float passiveDistance = 40f;
    public float jumpTime = .5f;
    public float jumpDistance = 3f;
    public AnimationCurve jumpHeightCurve;
    public float jumpHeight = 3f;
    public float jumpDelay = .5f;
    public float tiltMultiplier = 35f;
    public AnimationCurve jumpTiltCurve;
    private GameObject player;
    private bool chasing;
    private float lastJumpTime = -100;
    private Vector3 jumpDirection;
    private bool wasJumpingLastFrame = false;
    private UnityEngine.CharacterController characterController;
    private Vector3 ZeroY(Vector3 v) {
        return new Vector3(v.x, 0, v.z);
    }
    void OnEnable() {
        player = GameObject.Find("Vampire").gameObject;
        characterController = GetComponent<UnityEngine.CharacterController>();
    }
    void Update()
    {
        if (chasing) {
            transform.LookAt(player.transform.position);
        }
        float jumpProgress = (Time.time - lastJumpTime) / jumpTime;
        bool jumping = jumpProgress <= 1 && jumpProgress >= 0;
        if (!jumping && wasJumpingLastFrame) {
            modelOffset.transform.localPosition = Vector3.zero;
            modelOffset.transform.localRotation = Quaternion.identity;
        } else if (jumping) {
            characterController.Move(jumpDirection * Time.deltaTime * jumpDistance / jumpTime);
            transform.position -= Vector3.up * transform.position.y; // Ensure y is always 0
            modelOffset.localRotation = Quaternion.Euler(jumpTiltCurve.Evaluate(jumpProgress) * tiltMultiplier, 0, 0);
            modelOffset.localPosition = Vector3.up * jumpHeightCurve.Evaluate(jumpProgress) * jumpHeight;
        }
        wasJumpingLastFrame = jumping;

        var playerPosNoY = ZeroY(player.transform.position);
        var posNoY = ZeroY(transform.position);

        var toPlayerNoY = playerPosNoY - posNoY;
        float dist = toPlayerNoY.magnitude;
        if (chasing && dist >= passiveDistance) {
            chasing = false;
        } else if (!chasing && dist <= activateDistance) {
            chasing = true;
        }

        if (chasing) {
            if (Time.time > lastJumpTime + jumpDelay + jumpTime) {
                if (dist > jumpDistance * 1.5f) {
                    lastJumpTime = Time.time;
                    jumpDirection = toPlayerNoY.normalized;
                }
            }
        }
    }
}
