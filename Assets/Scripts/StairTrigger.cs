using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairTrigger : MonoBehaviour {
  private StairsManager parentManager;

  void Awake() {
    parentManager = GetComponentInParent<StairsManager>();
    if (parentManager == null) {
      Debug.LogError($"The Parent Stair Object for {gameObject.name} does not have a StairsManager script!");
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag(parentManager.player.tag))
      HandleTriggerEvent();
  }

  private void HandleTriggerEvent() { 
    if (gameObject.tag == "TopStairs") {
      if (!parentManager.isGoingUp && !parentManager.isGoingDown) {
        parentManager.isGoingDown = true;
        parentManager.playerAnimator.SetBool("isGoingDownStairs", true);
        return;
      }
      if (parentManager.isGoingUp) {
        parentManager.isGoingUp = false;
        parentManager.playerAnimator.SetBool("isGoingUpStairs", false);
        return;
      }
    }
    if (gameObject.tag == "BottomStairs") {
      if (!parentManager.isGoingUp && !parentManager.isGoingDown) {
        parentManager.isGoingUp = true;
        parentManager.playerAnimator.SetBool("isGoingUpStairs", true);
        return;
      }
      if (parentManager.isGoingDown) {
        parentManager.isGoingDown = false;
        parentManager.playerAnimator.SetBool("isGoingFalseStairs", false);
        return;
      }
    }
  }
}
