using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2_Step1 : MonoBehaviour
{
    // KILLING GOBLINS
    public int numberOfGoblins;
    public bool Completed = false;

    public GameObject nextStep;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("quest_2_step_1_completed") == 0)
        {
            int count = 0;
            foreach (Transform child in transform)
            {
                if (child.gameObject.tag == "Enemy")
                count += 1;
            }

            numberOfGoblins = count;

            if (numberOfGoblins == 0)
            {
                PlayerPrefs.SetInt("quest_2_step_1_completed", 1);
                Completed = true;
                nextStep.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
