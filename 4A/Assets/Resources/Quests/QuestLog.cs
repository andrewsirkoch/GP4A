using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{
    public bool clearQuestHistory;
    
    void Awake()
    {
        if(clearQuestHistory) ClearQuestHistory();
        //thirdQuest();

        // Quest One
        if (!PlayerPrefs.HasKey("quest_1_completed")) PlayerPrefs.SetInt("quest_1_completed", 0);
        if (!PlayerPrefs.HasKey("quest_1_step_1_completed")) PlayerPrefs.SetInt("quest_1_step_1_completed", 0);
        if (!PlayerPrefs.HasKey("quest_1_step_2_completed")) PlayerPrefs.SetInt("quest_1_step_2_completed", 0);

        // Quest Two
        if (!PlayerPrefs.HasKey("quest_2_completed")) PlayerPrefs.SetInt("quest_2_completed", 0);
        if (!PlayerPrefs.HasKey("quest_2_step_1_completed")) PlayerPrefs.SetInt("quest_2_step_1_completed", 0);
        if (!PlayerPrefs.HasKey("quest_2_step_2_completed")) PlayerPrefs.SetInt("quest_2_step_2_completed", 0);

        // Quest Three
        if (!PlayerPrefs.HasKey("quest_3_completed")) PlayerPrefs.SetInt("quest_3_completed", 0);
        if (!PlayerPrefs.HasKey("quest_3_step_1_completed")) PlayerPrefs.SetInt("quest_3_step_1_completed", 0);
        if (!PlayerPrefs.HasKey("quest_3_step_2_completed")) PlayerPrefs.SetInt("quest_3_step_2_completed", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ClearQuestHistory()
    {
        // Quest One
        PlayerPrefs.SetInt("quest_1_completed", 0);
        PlayerPrefs.SetInt("quest_1_step_1_completed", 0);
        PlayerPrefs.SetInt("quest_1_step_2_completed", 0);

        // Quest Two
        PlayerPrefs.SetInt("quest_2_completed", 0);
        PlayerPrefs.SetInt("quest_2_step_1_completed", 0);
        PlayerPrefs.SetInt("quest_2_step_2_completed", 0);


        PlayerPrefs.SetInt("quest_3_completed", 0);
        PlayerPrefs.SetInt("quest_3_step_1_completed", 0);
        PlayerPrefs.SetInt("quest_3_step_2_completed", 0);
    }
}
