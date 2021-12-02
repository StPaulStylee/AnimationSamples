using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkwayController : MonoBehaviour
{
  [SerializeField]
  private Animator animator;

  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      Debug.Log("Running that shit.");
      animator.SetBool("isExtended", true);
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.CompareTag("Player")) {
      animator.SetBool("isExtended", false);
    }
  }
}
