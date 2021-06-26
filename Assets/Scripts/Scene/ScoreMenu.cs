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
        scoreText.text = "Has obtenido " + PlayerStats.gold + " puntos.";
        
        PlayerStats.level += 1;
        PlayerStats.gold = 0;        
    }
}
