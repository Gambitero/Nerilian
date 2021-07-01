using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{    
    void Start()
    {        
        if (!MobileChecker.isMobile()){
            gameObject.SetActive(false);
        }
    }
}
