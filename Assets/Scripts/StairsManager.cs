using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour {

  public GameObject player;
  public ThirdPersonController playerController;
  public Animator playerAnimator;
  public bool isGoingUp { get; set; } = false;
  public bool isGoingDown { get; set; } = false;

  // Start is called before the first frame update
  void Start() {
    if (playerController == null || playerAnimator == null) {
      Debug.LogError("References missing in stair manager!");
    }
  }

  private void OnTriggerEnter(Collider other) {
    //if (other.gameObject.CompareTag("Player")) {
    //  Debug.Log("Triggered");
    //  var player = other.gameObject.GetComponent<ThirdPersonController>();
    //  player.isUsingStairs = !player.isUsingStairs;
    //  if (player.isUsingStairs) {
    //    player.animator.SetBool("isGoingUpStairs", true);
    //  } else {
    //    player.animator.SetBool("isGoingUpStairs", false);
    //  }
    //}
  }
}
