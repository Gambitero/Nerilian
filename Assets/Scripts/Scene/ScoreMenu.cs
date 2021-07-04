using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    public Text scoreText;
    public Text levelText;    
    void Awake()
    {
        if (LanguageSystem.languageValue == 0){
            levelText.text = "NIVEL " + PlayerStats.level;
            scoreText.text = "PUNTOS OBTENIDOS: " + PlayerStats.gold;
        }
        else{
            levelText.text = "LEVEL " + PlayerStats.level;
            scoreText.text = "SCORE: " + PlayerStats.gold;
        }
        
        PlayerStats.level += 1;
        PlayerStats.gold = 0;        
    }
}
