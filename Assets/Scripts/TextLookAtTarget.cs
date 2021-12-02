using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAtTarget : MonoBehaviour {
  [SerializeField]
  private GameObject target;

  // Start is called before the first frame update
  void Start() {
    if (target == null) {
      Debug.LogError($"The TMP object \"{gameObject.transform.parent.name}\" as no target to rotate towards.");
    }
  }

  // Update is called once per frame
  void Update() {
    transform.LookAt(target.transform);
    //LookAtTarget();
  }

  //private void LookAtTarget() {
  //  Vector3 direction = (target.transform.position + transform.position).normalized;
  //  float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
  //  transform.eulerAngles = Vector3.up * (angle + 180);
  //}
}
