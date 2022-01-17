using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ThirdPersonController : MonoBehaviour {
  [SerializeField]
  private float walkSpeed = 5.0f;
  [SerializeField]
  private float rotationSpeed = 100.0f;
  
  private Rigidbody rb;
  private Animator animator;

  private Vector3 desiredVelocity;

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
    float translation = Input.GetAxis("Vertical") * walkSpeed * Time.deltaTime;
    float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
    Move(translation, rotation);
  }

  private void Move(float translation, float rotation) {
    transform.Rotate(0, rotation, 0);
    animator.SetFloat("speed", GetSpeed(translation));
    if (translation != 0) {
      animator.SetBool("isWalking", true);
    } else {
      animator.SetBool("isWalking", false);
    }
  }

  private float GetSpeed(float translation) {
    return translation > 0 ? 1 : -1;  
  }
}
