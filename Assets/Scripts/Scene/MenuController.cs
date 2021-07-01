using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    void Start()
    {
        if(gameObject.name.Equals("Slider"))
            slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
    }
    public void SetAudioLevel(float sliderValue)
    {
        mixer.SetFloat("AudioVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("AudioVol", sliderValue);
    }
    public void GoTo(GameObject canvas) {
        canvas.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void LoadNewScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadNextScene()
    {
        if (PlayerStats.level < 4)
            SceneManager.LoadScene("Scene " + PlayerStats.level);

        else
            SceneManager.LoadScene("Credits");

    }
    public void setPlayerStats()
    {
        Controller.Cindex = Selector.classListIterator;
        Controller.Pindex = Selector.powerListIterator;
    }

    public void SetPlayerLevel(int lvl){
        PlayerStats.level = lvl;
    }
}
