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
    public void Life()
    {
        if (controler.coin >= 15)
        {
            controler.coin -= 10;
            controler.life += 3;
        }
        else
        {
            Debug.LogWarning("Moedas Insuficiente");
        }
    }

    public void espada()
    {
        if (controler.coin >= 20)
        {
            controler.coin -= 15;
            controler.Espada2Liberada = true;
            controler.SwoordF.SetActive(true);
            controler.SwoordM.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Moedas Insuficientes");
        }
    }

    public void Arco()
    {
        if (controler.coin >= 25)
        {
            controler.coin -= 20;
            controler.ArcoCanvas.SetActive(true);
            controler.ArcoLiberado = true;
            controler.flechas += 10;
        }
        else
        {
            Debug.LogWarning("Moedas Insuficiente");
        }
    }
    public void Flechas()
    {
        if (controler.coin >= 25 && controler.ArcoLiberado == true)
        {
            controler.coin -= 25;
            controler.flechas += 30;
        }
        else if (controler.ArcoLiberado == false)
        {
            Debug.LogWarning("Você Precisa Ter O arco Para comprar flechas");
        }
        {
            Debug.LogWarning("Moedas Insuficiente");
        }
    }

    public void pontodevida()
    {
        if (controler.coin >= 30)
        {
            controler.coin -= 30;
            controler.Vida += 2;
        }
        else
        {
            Debug.LogWarning("Moedas Insuficiente");
        }
    }

    public void Keys()
    {
        if (controler.coin >= 20 && controler.keysInventory < 1)
        {
            controler.coin -= 20;
            controler.keysInventory += 1; 
            controler.KeysC.SetActive(true);
        }
        else if (controler.keysInventory > 0)
        {
            Debug.LogWarning("Você Já Possui uma chave");
        }
        else
        {
            Debug.LogWarning("Moedas Insuficiente");
        }
    }
        
}
