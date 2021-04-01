using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    protected Animator anim;
    protected Rigidbody2D rb;

    [SerializeField] protected float time;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<Animator>().GetInteger("State") == 3)
            {
                rb.velocity = Vector2.zero;
                anim.SetBool("Death",true);
                Destroy(this.gameObject, time);
            }
        }
    }
        
}
