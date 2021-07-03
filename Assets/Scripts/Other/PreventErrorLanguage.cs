using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreventErrorLanguage : MonoBehaviour
{

    private void Start()
    {
        gameObject.GetComponent<Dropdown>().value = LanguageSystem.languageValue;
    }
    public void changeLanguage()
    {
        LanguageSystem.languageValue = gameObject.GetComponent<Dropdown>().value;
    }


    
}
