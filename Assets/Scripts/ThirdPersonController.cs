using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ThirdPersonController : MonoBehaviour {
  //[SerializeField]
  //private float walkSpeed = 5.0f;
  //[SerializeField]
  //private float sprintSpeed = 40.0f;
  [SerializeField]
  private float rotationSpeed = 100.0f;
  
  private Rigidbody rb;
  private Animator animator;

  private bool isRunning = false;
  private bool isJumping = false;

  private void Awake() {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
  }

  private void Update() {
    SetMovement();
    Animate();
    //if (Input.GetButtonDown("Jump")) {

    //}
  }

  private void Animate() {
    //if (desiredVelocity != Vector3.zero) {
    //  animator.SetBool("isWalking", true);
    //} else {
    //  animator.SetBool("isWalking", false);
    //}
  }

  private void SetMovement() {
    float translation;
    float rotation;
     translation = Input.GetAxis("Vertical");
     rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    if (Input.GetButton("Sprint") && translation != 0) {
      isRunning = true;
    } else {
      isRunning = false;
    }
    if (Input.GetButtonDown("Jump")) {
      if (isJumping) {
        return;
      }
      isJumping = true;
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
    transform.Rotate(0, rotation, 0);
    animator.SetFloat("speed", GetSpeed(translation));
    if (isRunning) {
      animator.SetBool("isWalking", false);
      animator.SetBool("isRunning", true);
    }
    else if (!isRunning && translation != 0){
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

  private void ResetJumpState() {
    isJumping = false;
  }
}
