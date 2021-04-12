using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private LayerMask ground;
    
    private int coin;
    [SerializeField] private Text coincounter;
    
    private enum State {idle, run, jump, fall, hit, doublejump, push, sword, attack, defeat}
    private State state = State.idle;
    
    private float speed = 8;
    private float jumpforce = 10;
    private float hurtforce = 6;
    
    public int health = 3;
    [SerializeField] private Text lives;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        ground = LayerMask.GetMask("ground");



    }
    void Update()
    {
        if (state != State.hit )
        {
            movement();

        }

        statemachine();
        anim.SetInteger("State", (int)state);

    }

    private void movement()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);            
        }
        
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);            
        }
        
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
        
    }

    private void statemachine()
    {
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.fall;
            }
        }
        
        else if (state == State.fall)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if (state == State.hit)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
            
            lives.text = health.ToString();
            
            if (health == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;
        }
        

        else
        {
            state = State.idle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectables")
        {
            Destroy(collision.gameObject);
            coin += 1;
            coincounter.text = coin.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(state == State.fall)
            {
                Jump();
            }

            else
            {
                health -= 1;
                state = State.hit;
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtforce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtforce, rb.velocity.y);

                }
            }        
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpforce);
        state = State.jump;
    }
}
