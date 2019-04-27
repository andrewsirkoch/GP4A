using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Preferences : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("volume_music")) PlayerPrefs.SetFloat("volume_music", 1);
        if (!PlayerPrefs.HasKey("volume_sfx")) PlayerPrefs.SetFloat("volume_sfx", 1);
        if (!PlayerPrefs.HasKey("volume_amb")) PlayerPrefs.SetFloat("volume_amb", 1);
    }

    // Update is called once per frame
    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        foreach (Slider slider in GetComponentsInChildren<Slider>())
        {
            slider.value = 1.0f;
        }
        foreach (DisplayPref text in GetComponentsInChildren<DisplayPref>())
        {
            text.UpdateText();
        }
    }
}
