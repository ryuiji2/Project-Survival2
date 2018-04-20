using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollider : MonoBehaviour {

    public List<Collider> gates = new List<Collider> ();
    public Collider player;

    void Start () {
        foreach (Collider c in gates) {
            Physics.IgnoreCollision (c, player);
        }
    }
}

