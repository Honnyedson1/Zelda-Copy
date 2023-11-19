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

    private void Start()
    {
        anim = GetComponent<Animator>();
        controler = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && controler.HasKey())
        {
            controler.UseKey();
            anim.SetTrigger("Open");
            OpenChest();
        }
    }
    private void OpenChest()
    {
        if (items.Length >= 0)
        {
            controler.KeysC.SetActive(false);
            int randomIndex = Random.Range(0, items.Length);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<BauRandon>());
            GameObject selectedItem = items[randomIndex];
            Instantiate(selectedItem, transform.position, Quaternion.identity);
            items = items.Where((item, index) => index != randomIndex).ToArray();
        }
    }
}