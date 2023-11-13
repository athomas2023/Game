using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
    }

public void ChangeVolume()
{
    AudioListener.volume = volumeSlider.value;
    Save();
}

private void Load()
{
    volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
}

private void Save()
{
    PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
}

public void MuteToggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
