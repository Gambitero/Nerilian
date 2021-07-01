using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]public AudioSource walk, jump, laser, zombieWalk, energy, spawnPoint, score, turning;
    static AudioSource audioSrc;
    void Start()
    {        
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound (string clip){
        switch(clip){
            case "laser":
                //audioSrc.PlayOneShot(laser);
                break;
        }
    }
}
