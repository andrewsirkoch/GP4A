using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Quest : MonoBehaviour
{
    public static string QuestTitle;
    public static string StepDescription;

    public void Update()
    {
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            if (text.gameObject.name == "Title")
            {
                text.text = QuestTitle;
            }
            else if (text.gameObject.name == "Description")
            {
                text.text = StepDescription;
            }
        }
        UpdateDescription();
    }

    void UpdateDescription()
    {
        int questNumber = QuestManager.currentQuest.GetComponent<Quest>().questNumber;
        int numberOfSteps = QuestManager.currentQuest.GetComponent<Quest>().numberOfSteps;
        for (int i = 1; i <= numberOfSteps; i++)
        {
            if(QuestManager.currentQuest.transform.Find("Quest" + questNumber.ToString() + "_Step" + i.ToString()) != null)
            {
                if (PlayerPrefs.GetInt("quest_" + questNumber.ToString() + "_step_" + i.ToString() + "_completed") == 0)
                {
                    StepDescription = QuestManager.currentQuest.transform.Find("Quest" + questNumber.ToString() + "_Step" + i.ToString()).GetComponent<Quest_Step>().description;
                    return;
                }
            }
        }
    }

}
