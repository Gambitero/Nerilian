using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DictionaryEngEsp : MonoBehaviour
{
    public GameObject SettingControl;
    List<string> textos = new List<string>{"Volver", "Empezar", "Jugar", "Opciones", "Volumen", "Idioma",
                        "Mundo 1", "Mundo 2", "Mundo 3", "Clase", "Continuar", "Guerrero", "Pícaro", "Mago", "Normal",
                         "Poder", "Dash", "Nada", "Salto doble","Puntos obtenidos:","Nivel", "Salir al menú",
                         "General", "Efectos", "Música"};
    List<string> texts = new List<string>{"Back", "Start", "Play", "Options", "Volume", "Language",
                        "World 1", "World 2", "World 3", "Class", "Continue", "Warrior", "Rogue", "Wizard", "Normal",
                         "Power", "Dash", "Nothing", "Double jump","Obtained points:","Level", "Exit to menu",
                         "General", "Effects", "Music"};

    private void Awake()
    {
        SettingControl = GameObject.Find("SettingsPersistent");
    }
    private void Update()
    {
            if (SettingControl.GetComponent<LanguageSystem>().languageValue == 0) {

                foreach (string x in texts)
                {
                    if (gameObject.GetComponent<Text>().text == x)
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
