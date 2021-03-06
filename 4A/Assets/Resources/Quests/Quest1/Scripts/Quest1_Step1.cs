﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest1_Step1 : MonoBehaviour
{
    // KILLING BOARS
    public int numberOfBoars;
    public bool Completed = false;

    public GameObject nextStep;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("quest_1_step_1_completed") == 0)
        {
            int count = 0;
            foreach (Transform child in transform)
            {
                count += 1;
            }

            numberOfBoars = count;

            if (numberOfBoars == 0)
            {
                PlayerPrefs.SetInt("quest_1_step_1_completed", 1);
                Completed = true;
                nextStep.SetActive(true);
            }
        }
    }
}
