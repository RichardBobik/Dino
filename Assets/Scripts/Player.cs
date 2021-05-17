using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    private enum State {idle, running, jumping, falling, hurt}
    private State state = State.idle;

    [SerializeField] private float jumpfoce = 5f;
    [SerializeField] private float speed = 5f;

    //Adjust the delay on turning off the jump feature
    [SerializeField] private float coyote = 0.2f;
    private float coyotetimer;
    [SerializeField] private LayerMask ground;
    [SerializeField] private int stars = 0;
    [SerializeField] private TextMeshProUGUI numberOfStars;
    [SerializeField] private float hurtForce = 5f;
    [SerializeField] private float killJump = 5f;
    //Footsteps tweeks
    [SerializeField] private ParticleSystem footstepAnymation;
    private ParticleSystem.EmissionModule footstepEmission;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource star;

    // Health system
    [SerializeField] private int health;
    [SerializeField] private int numberOfHearts;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite redHeart;
    [SerializeField] private Sprite purHeart;
    

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        footstepEmission = footstepAnymation.emission;
    }

    private void Update() {
        if (state != State.hurt) {
        Movement();    
        } 
        VelocityState();
        anim.SetInteger("state", (int)state);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        //Start the hurt process unles the collision comes from above.
        if (other.gameObject.tag == "Enemy") {
            if (other.contacts[0].normal.y < 0.5f)
            {
                state = State.hurt;
                HealthCalc();
                if (other.gameObject.transform.position.x > transform.position.x) {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else {
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            } 
            else {
            rb.velocity = new Vector2(rb.velocity.x, killJump);
            }  
            
            
        }
    }

    private void Movement() {

        float hDirection = Input.GetAxis("Horizontal");

        if (hDirection < 0) {
            rb.velocity = new Vector2 (-speed, rb.velocity.y);
            transform.localScale = new Vector2 (-2.1386f, 2.1386f);
        }
        else if (hDirection > 0) {
            rb.velocity = new Vector2 (speed, rb.velocity.y);
            transform.localScale = new Vector2 (2.1386f, 2.1386f);
        }
        //Adds an extra time for the player to jump to improve the gameplay.
        if (coll.IsTouchingLayers(ground)){
            coyotetimer = coyote;
        }
        else {
            coyotetimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && coyotetimer > 0) {
            Jump();
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Jump(){
    rb.velocity = new Vector2(rb.velocity.x, jumpfoce);
    state = State.jumping;
    }

    private void VelocityState() {
        if (state == State.jumping) {
            if (rb.velocity.y < 0f) {
                state = State.falling;
            }
        }
        else if (state == State.falling) {
            if (coll.IsTouchingLayers(ground)) {
                state = State.idle;
            }
        }
        else if (state == State.hurt) {
            // Matf.Abs gets the absolute velocity number so it works on negative values as well
            if (Mathf.Abs(rb.velocity.x) < 0.1f) {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f) {
            state = State.running; 
            }
        else {
            state = State.idle;     
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Collectables logic.   
        if (collision.tag == "Collectable") {
            star.Play();
            Destroy(collision.gameObject);
            stars ++;
            numberOfStars.text = stars.ToString();
            if (stars >= 100) {
                jumpfoce = 20;
            }
        }
        if (collision.tag == "HealthUp") {
            Destroy(collision.gameObject);
            if (health < 3) {
                HealthCalcPlus();
            }

        }
    }

    private void Footsteps() {
        footsteps.Play();

        if (Input.GetAxisRaw("Horizontal") != 0 && coll.IsTouchingLayers(ground)) {
            footstepEmission.rateOverTime = 30f;
        }
        else {
            footstepEmission.rateOverTime = 0f;
        }
    }


    private void HealthCalc() {
        health -= 1;
        if (health > numberOfHearts){
            health = numberOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].sprite = redHeart;
            }
            else {
                hearts[i].sprite = purHeart;
            }
        }
        if (health <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void HealthCalcPlus(){
        health += 1;
        if (health > numberOfHearts){
            health = numberOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health) {
                hearts[i].sprite = redHeart;
            }
            else {
                hearts[i].sprite = purHeart;
            }
        }
    }

}
