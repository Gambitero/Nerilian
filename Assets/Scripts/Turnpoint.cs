using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnpoint : MonoBehaviour
{
    // turnDir indica el sentido de la rotación de 90, se asigna a cada objeto turnPoint desde el editor de Unity
    // debe tomar valor +1 o -1, según se quiera rotar hacia la derecha o la izquierda respectivamente.
    [SerializeField] public int turnDir = 0;
}
