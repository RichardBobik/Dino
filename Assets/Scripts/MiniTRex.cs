using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTRex : Enemy
{
    [SerializeField]private float leftCap;
    [SerializeField]private float rightCap;
    [SerializeField]private float speed = 5f;

    private Collider2D coll;
    private AudioSource aud;
    private bool facingRight = true;

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        aud = GetComponent<AudioSource>();
    }
    void Update()
    {
        Move(); 
    }

    private void Move(){
        //Horizontal enemy movent, includes changing the sprite depending on the current direction
        if(facingRight) {
            rb.velocity = new Vector2 (speed, rb.velocity.y);

            if (transform.position.x >= rightCap) {
            transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
            facingRight = false;
            }
        }
        if (facingRight == false) {
            rb.velocity = new Vector2 (-speed, rb.velocity.y);

            if (transform.position.x <= leftCap) {
                transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
                facingRight = true;
            }
        }
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
