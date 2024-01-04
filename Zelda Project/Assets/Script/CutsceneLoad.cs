using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneLoad : MonoBehaviour
{
    private Player playerCutscene;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerCutscene = FindObjectOfType<Player>();
    }
    
    void LateUpdate()
    {
        if (playerCutscene.trocou == true)
        {
            StartCoroutine("PlayCutscene");
        }
    }

    IEnumerator PlayCutscene()
    {
        playerCutscene.speed = 0f;
        anim.SetBool("Activation", true);
        playerCutscene.animator.SetInteger("Transition", 0);
        yield return new WaitForSeconds(0.8f);
        playerCutscene.walkLiberado = false;
        playerCutscene.trocou = false;
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("Activation", false);
        playerCutscene.walkLiberado = true;
        playerCutscene.speed = 5f;
    }
}
