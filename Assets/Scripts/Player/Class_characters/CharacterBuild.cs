using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuild : MonoBehaviour
{
    public enum clases {
        Vulkan = 1,
        Herzs = 2,
        Normal = 0
    } //to do
    public enum powerUps {
        No = 0,
        Dash = 1,
        Reactor = 2      
    } //to do

    public List<clases> CharacterClass = new List<clases>() { clases.Normal, clases.Vulkan, clases.Herzs };
    public List<powerUps> CharacterPowerUps = new List<powerUps>(){powerUps.No, powerUps.Dash, powerUps.Reactor};
    public GameObject PlayerCharacter;

    public void PersonajeBuilder(clases clase, powerUps powerUp, GameObject playerCharacter)
    {
        var playerController = playerCharacter.GetComponent<Controller>();        

        float[] Stats = ClassStatsSelector(clase);

        playerController.speed = Stats[0];
        playerController.jumpHeight = Stats[1];
        playerController.weight = Stats[2];
        PlayerStats.lives = (int) Stats[3];
        PlayerStats.gold = 0;
        playerController.dashFlag = powerUp.Equals(powerUps.Dash);
        playerController.bunnyFlag = powerUp.Equals(powerUps.Reactor);

    }

    private float[] ClassStatsSelector(clases clase) {

        switch (clase)
        {            
            case clases.Vulkan:
                return GuerreroStats;
            case clases.Herzs:
                return PicaroStats;
            case clases.Normal:
                return NormalStats;
        }
        return null;
    }
    //Stats de las clases - TO DO cambiar segun clase
    private float[] GuerreroStats = {    
        //Speed
        10.0f,
        //Jumpforce
        4.0f,
        //Weight
        1.5f,
        //lives
        3,
        
    };
    private float[] PicaroStats = {
        //Speed
        14.0f,
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
        3.0f,
        //Weight
        1.2f,
        //lives
        4
    };


}
