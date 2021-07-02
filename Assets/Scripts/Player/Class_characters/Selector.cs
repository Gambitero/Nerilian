using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public static int powerListIterator = 0;
    public static int classListIterator = 0;
    public GameObject classText;
    public GameObject powerText;

    CharacterBuild builder;
    public void powerIterPlus() {
        powerListIterator += 1;
        if (powerListIterator > 2)
        {
            powerListIterator = 0;
        }   
    }
    public void powerIterLess() {
        powerListIterator -= 1;
        if (powerListIterator < 0)
        {
            powerListIterator = 2;
        }
    }
    public void classIterPlus() {
        classListIterator += 1;
        if (classListIterator > 2)
        {
            classListIterator = 0;
        }
    }
    public void classIterLess()
    {
        classListIterator -= 1;
        if (classListIterator < 0)
        {
            classListIterator = 2;
        }
    }

    private void Update()
    {
        builder = GetComponent<CharacterBuild>();
        classText.GetComponent<Text>().text = builder.CharacterClass[classListIterator].ToString().ToUpper();
        powerText.GetComponent<Text>().text = builder.CharacterPowerUps[powerListIterator].ToString().ToUpper();

    }
}
