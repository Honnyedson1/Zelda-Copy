using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BauRandon : MonoBehaviour
{
    public GameObject[] items;
    private Animator anim;
    private Player controler;
    public int keys;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controler = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keys >= 1)
        {
            keys -= 1;
            anim.SetTrigger("Open");
            OpenChest();
        }
    }

    public void SomarKeys()
    {
        keys++;
    }

    public void SubtrairKeys()
    {
        keys--;
    }

    private void OpenChest()
    {
        if (items.Length >= 0)
        {
            SubtrairKeys();
            controler.subtrairKeys(1);
            int randomIndex = Random.Range(0, items.Length);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<BauRandon>());
            GameObject selectedItem = items[randomIndex];
            Instantiate(selectedItem, transform.position, Quaternion.identity);
            items = items.Where((item, index) => index != randomIndex).ToArray();
        }
    }
}