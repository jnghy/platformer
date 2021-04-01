﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private float pos1;
    [SerializeField] private float pos2;
    [SerializeField] private float speed;

    private LayerMask ground;
    private bool facingleft;
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    
   /*
    private enum State {idle, walk, hit, death}
    private State state = State.idle;
    */

    
    void Start()
    {
        ground = LayerMask.GetMask("ground");
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        facingleft = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void move()
    {
        if (facingleft)
        {
            if (transform.position.x > pos1)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);

                }
            }
            else
            {
                
                facingleft = false;
            }
        }
        
        else
        {
            if (transform.position.x < pos2)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
            }
            else
            {
                facingleft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetBool("Death",true);
        }
    }
}
