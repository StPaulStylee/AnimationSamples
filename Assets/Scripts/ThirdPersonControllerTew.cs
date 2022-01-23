using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonControllerTew : MonoBehaviour {
  public float Acceleration = 0.1f;
  public float Deceleration;
  private Animator animator;
  private float velocity = 0.0f;
  private int velocityHash;

  private void Awake() {
    animator = GetComponent<Animator>();
    if (animator == null) {
      Debug.LogError($"No Animator on {gameObject.name}");
    }
    velocityHash = Animator.StringToHash("Velocity");
  }
  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    bool isWalkPressed = Input.GetKey(KeyCode.W);
    if (isWalkPressed && velocity < 1f) {
      velocity += Acceleration * Time.deltaTime;
    }
    if (!isWalkPressed && velocity > 0f) {
      velocity -= Deceleration * Time.deltaTime;
    }
    if (!isWalkPressed && velocity < 0f) {
      velocity = 0f;
    }
      animator.SetFloat(velocityHash, velocity);
  }
}
