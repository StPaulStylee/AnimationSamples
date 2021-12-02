using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
  [SerializeField]
  private Animator doorLeftAnimator;
  [SerializeField]
  private Animator doorRightAnimator;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag ("Player")) {
      doorLeftAnimator.SetBool("isOpen", true);
      doorRightAnimator.SetBool("isOpen", true);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      doorLeftAnimator.SetBool("isOpen", false);
      doorRightAnimator.SetBool("isOpen", false);
    }
  }
}
