using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ThirdPersonController : MonoBehaviour {
  public  Animator animator { get; private set; }

  public bool isRunning { get; set; }
  public bool isUsingStairs { get; set; } = false;
  [SerializeField]
  private float rotationSpeed = 100.0f;
  private Rigidbody rb;
  private int isWalkingHash, isRunningHash, idleJumpHash, walkingJumpHash, runningJumpHash;

  private void Awake() {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
    isWalkingHash = Animator.StringToHash("isWalking");
    isRunningHash = Animator.StringToHash("isRunning");
    idleJumpHash = Animator.StringToHash("idleJump");
    walkingJumpHash = Animator.StringToHash("walkingJump");
    runningJumpHash = Animator.StringToHash("runningJump");
  }

  private void Update() {
    SetMovementTew();
    //SetMovement();
    //Animate();
  }

  private void Animate() {
    //if (desiredVelocity != Vector3.zero) {
    //  animator.SetBool("isWalking", true);
    //} else {
    //  animator.SetBool("isWalking", false);
    //}
  }

  private void SetMovementTew() {
    bool isWalkPressed = Input.GetKey("w");
    bool isRunPressed = Input.GetButton("Sprint");
    bool isJumpPressed = Input.GetButtonDown("Jump");

    bool isWalking = animator.GetBool("isWalking");
    bool isRunning = animator.GetBool("isRunning");

    if (isWalkPressed && !isWalking) {
      animator.SetBool(isWalkingHash, true);
    }
    if (!isWalkPressed && isWalking) {
      animator.SetBool(isWalkingHash, false);
    }
    if (isRunPressed && isWalkPressed) {
      animator.SetBool(isRunningHash, true);
    }
    if (!isRunPressed || !isWalkPressed) {
      animator.SetBool(isRunningHash, false);
    }
    if (isJumpPressed && !isWalking) {
      animator.SetTrigger(idleJumpHash);
    }
    if (isJumpPressed && (isWalking && !isRunning)) {
      animator.SetTrigger(walkingJumpHash);
    }
    if (isJumpPressed && isRunning) {
      animator.SetTrigger(runningJumpHash);
    }
  }

  private void SetMovement() {
    float translation = Input.GetAxis("Vertical");
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    bool isRunPressed = Input.GetButton("Sprint");
    bool isJumpPressed = Input.GetButtonDown("Jump");

    if (isRunPressed && translation != 0) {
      isRunning = true;
    } else {
      isRunning = false;
    }
    if (isJumpPressed) {
      if (translation == 0) {
        animator.SetTrigger("idleJump");
        return;
      }
      if (isRunning) {
        animator.SetTrigger("runningJump");
        return;
      }
      animator.SetTrigger("walkingJump");
      return;
    }
    Move(translation, rotation);
  }

  private void Move(float translation, float rotation) {
    bool isWalkPlaying = animator.GetBool("isWalking");
    bool isRunPlaying = animator.GetBool("isRunning");
    transform.Rotate(0, rotation, 0);
    if (isRunning) {
      animator.SetBool("isWalking", false);
      animator.SetBool("isRunning", true);
    }
    else if (!isRunning && translation != 0){
      animator.SetFloat("speed", GetSpeed(translation)); 
      animator.SetBool("isWalking", true);
      animator.SetBool("isRunning", false);
    }
    else {
      animator.SetBool("isWalking", false);
      animator.SetBool("isRunning", false);
    }
  }

  private float GetSpeed(float translation) {
    return translation > 0 ? 1 : -1;  
  }
}
