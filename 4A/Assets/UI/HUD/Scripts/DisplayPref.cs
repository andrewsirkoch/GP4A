using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPref : MonoBehaviour
{
    public string PrefName;
    
    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(PrefName).ToString();
    }
}
