using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerLojinha : MonoBehaviour
{
    private Player controler;
    void Start()
    {
        controler = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Item1()
    {
        if (controler.coin >= 10)
        {
            controler.coin -= 10;
            controler.life += 3;
        }
        else
        {
            Debug.Log("Moedas Insuficiente");
        }
    }

    public void item2()
    {
        if (controler.coin >= 15)
        {
            controler.coin -= 15;
            controler.Espada2Liberada = true;
            controler.SwoordF.SetActive(true);
            controler.SwoordM.SetActive(false);
        }
    }
}
