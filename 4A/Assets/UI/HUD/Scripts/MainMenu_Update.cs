using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Update : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            if (slider.gameObject.name == "MusicSlider")
            {
                slider.value = PlayerPrefs.GetFloat("volume_music");
            }
            else if (slider.gameObject.name == "SFXSlider")
            {
                slider.value = PlayerPrefs.GetFloat("volume_sfx");
            }
            if (slider.gameObject.name == "AMBSlider")
            {
                slider.value = PlayerPrefs.GetFloat("volume_amb");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Slider[] sliders = gameObject.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            if (slider.gameObject.name == "MusicSlider")
            {
                PlayerPrefs.SetFloat("volume_music", slider.value);
            }
            else if (slider.gameObject.name == "SFXSlider")
            {
                PlayerPrefs.SetFloat("volume_sfx", slider.value);
            }
            if (slider.gameObject.name == "AMBSlider")
            {
                PlayerPrefs.SetFloat("volume_amb", slider.value);
            }
        }
    }
}
