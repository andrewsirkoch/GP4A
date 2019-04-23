using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!PlayerPrefs.HasKey("volume_music")) PlayerPrefs.SetFloat ("volume_music", 1);
        if (!PlayerPrefs.HasKey("volume_sfx")) PlayerPrefs.SetFloat("volume_sfx", 1);
        if (!PlayerPrefs.HasKey("volume_amb")) PlayerPrefs.SetFloat("volume_amb", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
