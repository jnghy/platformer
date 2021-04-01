﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamareController : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offset = new Vector3(3, 2, -10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}
