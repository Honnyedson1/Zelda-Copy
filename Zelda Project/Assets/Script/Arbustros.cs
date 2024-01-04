using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbustros : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Espada"|| col.gameObject.tag == "Espada2" )
        {
            Destroy(gameObject);
        }
    }
}
