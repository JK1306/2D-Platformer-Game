﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<PlayerController>() != null){
            player = collision.gameObject.GetComponent<PlayerController>();            
            player.addScore();
            Destroy(gameObject);
        }
    }
}
