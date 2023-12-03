using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour
{
    private Player Controler;
    public GameObject open;

    void Start()
    {
        Controler = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Controler.KeysDoors > 0)
        {
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            open.gameObject.SetActive(true);
            Controler.KeysDors.gameObject.SetActive(false);
        }
    }
}
