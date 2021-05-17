using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFly : Enemy
{

    [SerializeField]private float topCap;
    [SerializeField]private float botCap;
    [SerializeField]private float speed = 5f;
    private Collider2D coll; 
    private AudioSource aud;

    private bool facingTop = true;

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

    private void OnCollisionEnter2D(Collision2D other) {
        //Trigers the JumpedOn method from Enemy class if the enemy is jumped on from above.
        if (other.gameObject.tag == "Player") {
            if (other.contacts[0].normal.y < -0.5f) {
                JumpedOn();
            }

        }
    }

    private void Move(){
        //Vertical enemy movent, includes changing the sprite depending on the current direction
        if(facingTop) {
            rb.velocity = new Vector2 (rb.velocity.x, speed);

            if (transform.position.y >= topCap) {
            transform.localScale = new Vector3 (-0.5f, 0.5f, 0.5f);
            facingTop = false;
            }
        }
        if (facingTop == false) {
            rb.velocity = new Vector2 (rb.velocity.x, -speed);

            if (transform.position.y <= botCap) {
                transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
                facingTop = true;
            }
        }
    }
    private void DeathSound(){
        aud.Play();
    }
}
