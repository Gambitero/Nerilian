using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageSystem : MonoBehaviour
{
    public static int languageValue = 0;
    private void Awake()
    {
        int dontdestroys = GameObject.FindGameObjectsWithTag("undestroy").Length;
        if(dontdestroys != 1)
        {
            Destroy(this);
        }else
        DontDestroyOnLoad(gameObject);       
    }    

}
