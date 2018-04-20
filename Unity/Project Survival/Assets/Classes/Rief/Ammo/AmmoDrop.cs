using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour {

    public int ammo;
    public Shooting mp40;

    public void Update () {

        mp40 = FindObjectOfType<Shooting> ();

    }

    private void OnCollisionEnter (Collision ammoDrop) {
        if(ammoDrop.transform.tag == "AmmoDrop") {
            print ("hit the box");
            mp40.mp40AmmoTotal += ammo;
            Destroy (ammoDrop.gameObject);
        }
    }
}