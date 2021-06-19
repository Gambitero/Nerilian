using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryEngEsp : MonoBehaviour
{
    public GameObject SettingControl;
    List<string> textos = new List<string>{"Volver", "Empezar", "Jugar", "Opciones", "Volumen", "Idioma",
                        "Mundo 1", "Mundo 2", "Mundo 3"};
    List<string> texts = new List<string>{"Back", "Start", "Play", "Options", "Volume", "Language",
                        "World 1", "World 2", "World 3"};

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
