﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script regula las animaciones de la escena, los cambios de escena...
public class SceneController : MonoBehaviour
{
    public Animator animator;
    public GameObject spawnPoint;
    public string nextLevel;
    float count = 0f;
    float animLength;
    float animLength2;
    float delayLength;
    public bool waiting = false;
    public bool delay = false;

    public void GameOver(float l = 2f)
    {
        animLength = l;
        animator.SetTrigger("GameOver");
    }
    
    // Hace fade in o fade out, según el estado en el que se esté actualmente, el parámetro l debe contener la duración de la animación
    public void Fade(float l = 0f)
    {
        waiting = true;
        animLength = l;        
        animator.SetTrigger("FadeOut");        
        //animator.ResetTrigger("FadeOut");
    }
    
    // Desencadena un fade tras un tiempo de espera marcado por la variable del
    public void DelayedFade(float del = 0f, float l = 0f)
    {
        waiting = true;
        delay = true;
        animLength = del;
        animLength2 = l;        
    }

    // Si se cambia de spawnPoint dentro de la misma escena, se llama a este método y se le pasa el nuevo spawnPoint
    public void SetSpawnpoint(GameObject sP)
    {
        this.spawnPoint = sP;
    }

    public void Update()
    {
        if(waiting)
        {
            count += Time.deltaTime;
            if(count >= animLength)
            {
                waiting = false;
                count = 0f;
                if(delay)
                {
                    delay = false;
                    Fade(animLength2);
                }
            }
        }
    }
}