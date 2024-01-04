using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peaks : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.life -= 1;
        }
    }
}
