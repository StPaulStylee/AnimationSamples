using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour {
  [Header("Movement")]
  protected Vector3 desiredVelocity;
  public float MoveSpeed;

  [Header("Camera")]
  public float LookSensitivity = 4f; 
  public float MinLookX = -80f; 
  public float MaxLookX = 80f; 
  private float currentRotationX;

  private Camera cam;
  protected Rigidbody rb;

  private void Awake() {
    cam = Camera.main;
    rb = GetComponent<Rigidbody>();
    Cursor.lockState = CursorLockMode.Locked;
  }

  private void Update() {
    desiredVelocity = GetMoveInputs();
    CameraLook();
  }

  private Vector3 GetMoveInputs() {
    Vector2 playerInput;
    playerInput.x = Input.GetAxis("Horizontal");
    playerInput.y = Input.GetAxis("Vertical");
    playerInput = Vector2.ClampMagnitude(playerInput, 1f);
    // This takes the global values recieved above and makes them local to our player
    // This is necessary to correctly move in the direction we are facing
    // The transform.right/forward correspond to x axis (red) and z axis (blue)
    // Is there a way to do this in editor and not in code?
    Vector3 direction = (transform.right * playerInput.x + transform.forward * playerInput.y) * MoveSpeed;
    direction.y = rb.velocity.y; 
    return direction;
  }

  private void FixedUpdate() {
    Move(desiredVelocity);
  }

  private void Move(Vector3 direction) {
    rb.velocity = direction;
  }

  private void CameraLook() {
    float y = Input.GetAxis("Mouse X") * LookSensitivity; // Get Y rotation AROUND the x axis
    currentRotationX += Input.GetAxis("Mouse Y") * LookSensitivity; // Get X rotation AROUND the y axis
    currentRotationX = Mathf.Clamp(currentRotationX, MinLookX, MaxLookX); // Restrict x rotation (can only look up and down to set boundries)
    cam.transform.localRotation = Quaternion.Euler(-currentRotationX, 0, 0); // Apply x restriction (Note the negative in the x param, this makes in NOT INVERTED)
    transform.eulerAngles += Vector3.up * y; // Apply the y rotation to the player (Not the camera) so the player is always facing the correct way
  }
}
