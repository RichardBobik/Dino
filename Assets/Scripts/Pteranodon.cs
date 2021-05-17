using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pteranodon : Enemy
{
    private Collider2D coll; 

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();  
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Method can be turned on to make the enemy voulnarable. 
        if (other.gameObject.tag == "Player") {
            if (other.contacts[0].normal.y < -0.5f) {
                //JumpedOn();
            }

        }
    }
}

