using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuild : MonoBehaviour
{
    [SerializeField] public int initialLives = 3;
    [SerializeField] public float initialSpeed = 3;
    [SerializeField] public float initialJumpForce = 3;

    public enum clases { 
        nada
    } //to do
    public enum powerUps {
        nada
    } //to do

    //to do

    CharacterBuild()
    {
        int lives = initialLives;
        float Speed = initialSpeed;
        float JumpForce = initialJumpForce;
    }

    private CharacterBuild Personaje(clases clase, powerUps powerUp)
    {
        //construye personaje en funcion de la clase, genera atributos
        return new CharacterBuild();
    }


    private powerUps PowerUp1()
    {
        //hace cosas, renombrar a nombre de power up
        return new powerUps();
    }


}
