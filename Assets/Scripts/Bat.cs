using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    private Collider2D coll; 
    private AudioSource aud;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        aud = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Trigers the JumpedOn method from Enemy class if the enemy is jumped on from above.
        if (other.gameObject.tag == "Player") {
            if (other.contacts[0].normal.y < -0.5f) {
                JumpedOn();
            }

        }
    }
    private void DeathSound(){
        aud.Play();
    }
}
