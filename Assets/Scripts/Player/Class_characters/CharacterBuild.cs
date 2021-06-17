using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuild : MonoBehaviour
{
    public enum clases {
        Guerrero = 1,
        MagoRojo = 2,
        Berserker = 3,
        Picaro = 4,
        Normal = 0
    } //to do
    public enum powerUps {
        Nada = 0,
        BolaFuego = 1,
        Dash = 2,
        BunnyHop = 3      
    } //to do

    public clases CharacterClass;
    public powerUps CharacterPowerUps;
    public GameObject PlayerCharacter;

    public void PersonajeBuilder(clases clase, powerUps powerUp, GameObject playerCharacter)
    {
        var playerController = playerCharacter.GetComponent<Controller>();
        var playerStats = playerCharacter.GetComponent<PlayerStats>();

        float[] Stats = ClassStatsSelector(clase);

        playerController.speed = Stats[0];
        playerController.jumpHeight = Stats[1];
        playerController.weight = Stats[2];
        playerStats.lives = (int) Stats[3];
        playerController.dashFlag = powerUp.Equals(powerUps.Dash);
        playerController.shootFlag = powerUp.Equals(powerUps.BolaFuego);
        playerController.bunnyFlag = powerUp.Equals(powerUps.BunnyHop);

    }

    private float[] ClassStatsSelector(clases clase) {

        switch (clase)
        {            
            case clases.Guerrero:
                return GuerreroStats;
            case clases.MagoRojo:
                return MagoRojoStats;
            case clases.Berserker:
                return BerserkerStats;
            case clases.Picaro:
                return PicaroStats;
            case clases.Normal:
                return NormalStats;
        }
        return null;
    }
    //Stats de las clases - TO DO cambiar segun clase
    private float[] GuerreroStats = {    
        //Speed
        12.0f,
        //Jumpforce
        5.0f,
        //Weight
        1.0f,
        //lives
        3,
        //dash
        1,
        
    };
    private float[] MagoRojoStats = {
        //Speed
        12.0f,
        //Jumpforce
        2.0f,
        //Weight
        1.0f,
        //lives
        3
    };
    private float[] BerserkerStats = {
        //Speed
        12.0f,
        //Jumpforce
        2.0f,
        //Weight
        1.0f,
        //lives
        3
    };
    private float[] PicaroStats = {
        //Speed
        12.0f,
        //Jumpforce
        2.0f,
        //Weight
        1.0f,
        //lives
        3
    };
    private float[] NormalStats = {
        //Speed
        12.0f,
        //Jumpforce
        2.5f,
        //Weight
        1.0f,
        //lives
        3
    };


}
