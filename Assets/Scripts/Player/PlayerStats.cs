using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{    
    PlayerClass pClass;    
    int currentHp;
    static public int lives = 1;
    static public int gold;
    public int goldExchange = 100;

    public SceneController sceneController;
    public CharacterController controller;       

    // Se activa al ser atacado
    public void Attacked()
    {
        currentHp--;
        //inmunidad tras ser atacado durante un segundo
        if(currentHp==0)
        {
            controller.gameObject.GetComponent<Controller>().Die();
        }
    }

    // Coger una vida
    public void TakeLife()
    {
        lives++;
        controller.gameObject.GetComponent<Controller>().livesText.text = "x" + lives;
    }

    // Coger oro/puntos...
    public void TakeGold()
    {
        gold++;        
        if (gold >= goldExchange) {
            gold -= goldExchange;
            TakeLife();
        }
        controller.gameObject.GetComponent<Controller>().scoreText.text = "" + gold%goldExchange;
    }
}
