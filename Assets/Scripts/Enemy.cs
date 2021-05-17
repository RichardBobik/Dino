using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    protected Animator anim;
    protected Rigidbody2D rb;
    public enum Condition {alive, dead};

    protected virtual void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected void JumpedOn(){
        //Triggers death animation after enemy is destroyed.  Collider is turned off so the enemy can no longer interact with the player.
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    protected void Death() {
        Destroy(this.gameObject);
    }
}
