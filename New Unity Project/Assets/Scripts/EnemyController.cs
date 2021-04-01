using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
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
                anim.SetBool("Death",true);
            }
        }
    }
        
}
