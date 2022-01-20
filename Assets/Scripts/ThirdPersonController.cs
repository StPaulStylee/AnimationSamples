using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ThirdPersonController : MonoBehaviour {
  [SerializeField]
  private float rotationSpeed = 100.0f;
  
  private Rigidbody rb;
  public  Animator animator { get; private set; }

  public bool isRunning { get; set; }
  public bool isUsingStairs { get; set; } = false;

  private void Awake() {
    rb = GetComponent<Rigidbody>();
    animator = GetComponent<Animator>();
  }

  private void Update() {
    SetMovement();
    Animate();
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
