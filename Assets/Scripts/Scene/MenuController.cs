using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider generalSlider;
    public Slider musicSlider;
    public Slider fxSlider;

    void Start()
    {
        if (gameObject.name.Equals("generalSlider"))
        {
            generalSlider.value = PlayerPrefs.GetFloat("GeneralVolume", 0.75f);
        }
        if (gameObject.name.Equals("musicSlider"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        }
        if (gameObject.name.Equals("fxSlider"))
        {
            fxSlider.value = PlayerPrefs.GetFloat("fxVolume", 0.75f);
        }
    }
    public void SetGeneralAudioLevel(float sliderValue)
    {
        Mixer.SetFloat("generalVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("generalVol", sliderValue);
    }
    public void SetMusicAudioLevel(float sliderValue)
    {
        Mixer.SetFloat("musicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("musicVol", sliderValue);
    }
    public void SetFXAudioLevel(float sliderValue)
    {
        Mixer.SetFloat("fxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("fxVol", sliderValue);
    }
    public void GoTo(GameObject canvas) {
        canvas.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void LoadNewScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadSavedScene()
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
