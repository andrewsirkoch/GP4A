using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [Space]
    public int questNumber;
    [Space]
    public int numberOfSteps;
    [Space]
    public int coinReward;
    [Space]
    public bool Completed;
    private bool rewardSpawned;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("quest_" + questNumber.ToString() + "_completed") == 1) Completed = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Quest" + questNumber.ToString() + "_Step" + numberOfSteps.ToString())
            {
                if (child.gameObject.GetComponent<LastStep>().Completed)
                {
                    Completed = true;
                    PlayerPrefs.SetInt("quest_" + questNumber.ToString() + "_completed", 1);
                }
            }
        }

        if (Completed && !rewardSpawned)
        {
            rewardSpawned = true;
            QuestManager.SpawnCoins(coinReward);
            GameObject.Find("HUD_QuestComplete").GetComponent<HUD_questComplete>().flashMessage();
        }
    }
}
