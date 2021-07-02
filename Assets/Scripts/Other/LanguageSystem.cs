using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSystem : MonoBehaviour
{
    public Dropdown language;
    public int languageValue = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void changeLanguage()
    {

        // 0 español
        // 1 english

        languageValue = language.value;
    }

}
