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
        levelText.text = PlayerStats.level.ToString();
        scoreText.text = PlayerStats.gold.ToString();
        
        PlayerStats.level += 1;
        PlayerStats.gold = 0;        
    }
}
