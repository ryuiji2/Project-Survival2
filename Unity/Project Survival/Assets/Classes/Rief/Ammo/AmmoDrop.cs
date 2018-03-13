using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour {

    public int ammo;
    public Shooting mp40;

    private void OnCollisionEnter (Collision ammoDrop) {
        if(ammoDrop.transform.tag == "ammoDrop") {
            mp40 = FindObjectOfType<Shooting> ();
            mp40.mp40Ammo += ammo;
            Destroy (ammoDrop.gameObject);
        }
    }
}