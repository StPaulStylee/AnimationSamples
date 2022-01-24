using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonControllerThree : MonoBehaviour {
  public float Acceleration = 0.4f;
  public float Deceleration = 0.5f;
  public float MaxWalkVelocity = 0.5f;
  public float MaxRunVelocity = 2f;
  private Animator animator;
  private int velocityXHash;
  private int velocityZHash;
  private float velocityX = 0f;
  private float velocityZ = 0f;

  private void Awake() {
    animator = GetComponent<Animator>();
    if (animator == null) {
      Debug.LogError($"No Animator on {gameObject.name}");
    }
    velocityXHash = Animator.StringToHash("Velocity X");
    velocityZHash = Animator.StringToHash("Velocity Z");
  }

  // Update is called once per frame
  void Update() {
    bool isWalkPressed = Input.GetKey(KeyCode.W);
    bool isRunPressed = Input.GetKey(KeyCode.LeftShift);
    bool isLeftPressed = Input.GetKey(KeyCode.A);
    bool isRightPressed = Input.GetKey(KeyCode.D);
    float currentMaxVelocity = isRunPressed ? MaxRunVelocity : MaxWalkVelocity;

    SetMovementVelocity(isWalkPressed, isLeftPressed, isRightPressed, currentMaxVelocity);
    LockVelocityToCorrectValues(isWalkPressed,isLeftPressed,isRightPressed);

    animator.SetFloat(velocityZHash, velocityZ);
    animator.SetFloat(velocityXHash, velocityX);
  }

  private void SetMovementVelocity(bool isWalkPressed, bool isLeftPressed, bool isRightPressed, float currentMaxVelocity) {
    // Walking/Running
    if (isWalkPressed && velocityZ < currentMaxVelocity) {
      velocityZ += Acceleration * Time.deltaTime;
    }
    // Slowing down to a walk
    if (isWalkPressed && velocityZ >= currentMaxVelocity) {
      velocityZ -= Deceleration * Time.deltaTime;
    }
    // Walk Strafe left
    if (isLeftPressed && velocityX > -currentMaxVelocity) {
      velocityX -= Acceleration * Time.deltaTime;
    }
    // Run Strafe Left
    if (isLeftPressed && velocityX > -currentMaxVelocity) {
      velocityX -= Acceleration * Time.deltaTime;
    }
    // Walk strafe Right
    if (isRightPressed && velocityX < currentMaxVelocity) {
      velocityX += Acceleration * Time.deltaTime;
    }
    // Slow Down to Walk Strafe Left
    if (isLeftPressed && velocityX < -currentMaxVelocity) {
      velocityX += Deceleration * Time.deltaTime;
    }
    // Slow Down to Walk Strafe Right
    if (isRightPressed && velocityX > currentMaxVelocity) {
      velocityX -= Deceleration * Time.deltaTime;
    }
    // Run Strafe Right
    if (isRightPressed && velocityX < currentMaxVelocity) {
      velocityX += Acceleration * Time.deltaTime;
    }
    // Stop strafing left
    if (!isLeftPressed && velocityX < 0f) {
      velocityX += Deceleration * Time.deltaTime;
    }
    // stop strafing right
    if (!isRightPressed && velocityX > 0f) {
      velocityX -= Deceleration * Time.deltaTime;
    }
    // Stop walking/running
    if (!isWalkPressed && velocityZ != 0) {
      velocityZ -= Deceleration * Time.deltaTime;
    }
  }

  private void LockVelocityToCorrectValues(bool isWalkPressed, bool isLeftPressed, bool isRightPressed) {
    if (!isWalkPressed && velocityZ < 0f) {
      velocityZ = 0f;
    }
    if (!isLeftPressed && !isRightPressed && velocityX != 0 && (velocityX > -0.05f && velocityX < 0.05f)) {
      velocityX = 0f;
    }
  }
}
