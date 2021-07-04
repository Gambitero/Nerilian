using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryEngEsp : MonoBehaviour
{
    public GameObject SettingControl;
    List<string> textos = new List<string>{"VOLVER", "EMPEZAR", "JUGAR", "OPCIONES", "VOLUMEN", "IDIOMA",
                        "NIVEL 1", "NIVEL 2", "NIVEL 3", "CLASE", "CONTINUAR", "GUERRERO", "PICARO", "MAGO", "NORMAL",
                         "PODER", "DASH", "NADA", "SALTO DOBLE", "GUARDAR", "SALIR","GENERAL","MUSICA","EFECTOS", "SALIR AL MENU",
                        "PUNTOS OBTENIDOS:", "CREDITOS","PROGRAMACION Y SONIDO", "UX/UI Y PROGRAMACION", "CONCEPTO DE JUEGO Y NIVELES", "ARTE Y VISUAL",
                        "APRENDE A JUGAR"};
    List<string> texts = new List<string>{"BACK", "START", "PLAY", "OPTIONS", "VOLUME", "LANGUAGE",
                        "LEVEL 1", "LEVEL 2", "LEVEL 3", "CLASS", "CONTINUE", "WARRIOR", "ROGUE", "WIZARD", "NORMAL",
                         "POWER", "DASH", "NOTHING", "DOUBLE JUMP", "SAVE", "EXIT", "GENERAL", "MUSIC", "EFFECTS", "EXIT TO MENU",
                        "LEVEL SCORE:", "CREDITS", "PROGRAMMING AND SOUND", "UX/UI & PROGRAMMING", "GAME & LEVEL DESIGN", "ART & VISUAL", "LEARN HOW TO PLAY"};
    private void Update()
    {
        if (LanguageSystem.languageValue == 0) {
            foreach(string x in texts)
            {
                if(gameObject.GetComponent<Text>().text == x)
                {
                    gameObject.GetComponent<Text>().text = textos[texts.IndexOf(x)];
                }
            }        
        }
        if (LanguageSystem.languageValue == 1) {

            foreach (string x in textos)
            {
                if (gameObject.GetComponent<Text>().text == x)
                {
                    gameObject.GetComponent<Text>().text = texts[textos.IndexOf(x)];
                }
            }
        }
    }

}
