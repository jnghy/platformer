using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private LayerMask ground;
    
    public int coin;
    [SerializeField] private Text coincounter;
    
    private enum State {idle, run, jump, fall, doublejump, push, sword, attack, hit, defeat}
    private State state = State.idle;
    
    private float speed = 7;
    private float jumpforce = 7;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        ground = LayerMask.GetMask("ground");
    }
    void Update()
    {
        movement();
        statemachine();
        anim.SetInteger("State", (int)state);
    }

    private void movement()
    {
        // left
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);            
        }
        
        // right

        else if (Input.GetAxis("Horizontal") > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);            
        }
        
        // jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            state = State.jump;
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
            if (coll.IsTouchingLayers())
            {
                state = State.idle;
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
}
