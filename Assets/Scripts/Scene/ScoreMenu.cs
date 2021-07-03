using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;    
    void Start()
    {
        levelText.text = "NIVEL " + PlayerStats.level;
        scoreText.text = "HAS OBTENIDO " + PlayerStats.gold + " PUNTOS.";
        
        PlayerStats.level += 1;
        PlayerStats.gold = 0;        
    }
}
