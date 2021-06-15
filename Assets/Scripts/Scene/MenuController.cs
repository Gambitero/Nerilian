using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void GoTo(GameObject canvas) {
        Debug.Log("click");
        canvas.SetActive(true);
    
    }
}
