using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryEngEsp : MonoBehaviour
{
    public GameObject SettingControl;
    List<string> textos = new List<string>{"VOLVER", "EMPEZAR", "JUGAR", "OPCIONES", "VOLUMEN", "IDIOMA",
                        "MUNDO 1", "MUNDO 2", "MUNDO 3", "CLASE", "CONTINUAR", "GUERRERO", "PICARO", "MAGO", "NORMAL",
                         "PODER", "DASH", "NADA", "SALTO DOBLE", "GUARDAR", "SALIR"};
    List<string> texts = new List<string>{"BACK", "START", "PLAY", "OPTIONS", "VOLUME", "LANGUAGE",
                        "WORLD 1", "WORLD 2", "WORLD 3", "CLASS", "CONTINUE", "WARRIOR", "ROGUE", "WIZARD", "NORMAL",
                         "POWER", "DASH", "NOTHING", "DOUBLE JUMP", "SAVE", "EXIT"};

    private void Awake()
    {
        SettingControl = GameObject.Find("SettingsPersistent");
    }

    private void Update()
    {
        if (SettingControl.GetComponent<LanguageSystem>().languageValue == 0) { 
            
            foreach(string x in texts)
            {
                if(gameObject.GetComponent<Text>().text == x)
                {
                    gameObject.GetComponent<Text>().text = textos[texts.IndexOf(x)];
                }
            }
        
        }
        if (SettingControl.GetComponent<LanguageSystem>().languageValue == 1) {

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
