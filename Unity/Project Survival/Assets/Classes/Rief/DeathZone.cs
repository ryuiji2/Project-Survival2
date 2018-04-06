using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {


    public void OnTriggerEnter (Collider enemy) {
        if (enemy.gameObject.tag == "Enemy") {
            Destroy (enemy.transform.parent.gameObject);
        }
    }
}
