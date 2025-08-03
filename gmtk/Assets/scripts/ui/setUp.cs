using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class setUp : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer audioMixer;
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("bgm"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("bgm");
        }
        if (PlayerPrefs.HasKey("sound"))
        {
            soundSlider.value = PlayerPrefs.GetFloat("sound");
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("bgm", musicSlider.value);
        PlayerPrefs.SetFloat("sound", soundSlider.value);
    }
    public void setSoundVolume()
    {
        audioMixer.SetFloat("sound", Mathf.Log10(soundSlider.value) * 25);
    }
    public void setMusicVolume()
    {
        audioMixer.SetFloat("bgm", Mathf.Log10(musicSlider.value) * 25);
    }
}