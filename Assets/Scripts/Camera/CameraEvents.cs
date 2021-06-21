using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    public bool trueFollow;
    public bool backwards;
    public CameraWindow window;
    void OnTriggerEnter(Collider obj){        
        if(obj.CompareTag("Player")){            
            window.trueFollow = trueFollow;
            if (backwards)
                trueFollow = !trueFollow;
        }
    }
}
