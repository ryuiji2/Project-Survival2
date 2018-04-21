using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float moveSpeed;
    public float rotateSpeed;
    public bool canJump;
    public int jumpHeight = 3; //hoog zetten, werkt anders niet heel goed. 300 werkte prima.

    public bool block;

    void Start() {
        canJump = true;
    }

    void FixedUpdate() {

        if(!block) {

            CharMove();
            Jump ();
        }
    }

    void CharMove() {

        float forwardBack = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float leftRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        transform.Translate(leftRight, 0.0f, forwardBack);
    }

    void Jump () {

        if (Input.GetButtonDown ("Jump") && canJump) {
            GetComponent<Rigidbody> ().AddForce (0, jumpHeight * 100, 0);
            canJump = false;
        }
    }

    private void OnCollisionEnter (Collision collision) {
        
        if (collision.gameObject.tag == "Ground") {
            canJump = true;
        }
    }
}