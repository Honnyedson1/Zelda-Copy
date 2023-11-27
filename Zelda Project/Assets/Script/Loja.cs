using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loja : MonoBehaviour
{
    public GameObject lojaUI;
    private bool estaNaLoja = false;

    void Start()
    {
        lojaUI.SetActive(false);
    }

    void Update()
    {
        if (estaNaLoja == true)
        {
            Debug.Log("Dentro da loja!");
            lojaUI.SetActive(true);
        }
        else
        {
            Debug.Log("Fora da loja!");
            lojaUI.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Loja"))
        {
            estaNaLoja = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Loja"))
        {
            estaNaLoja = false;
        }
    }
}