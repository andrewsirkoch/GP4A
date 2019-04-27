using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public int numberOfQuests;
    public int currentQuestNumber;
    public static bool questSpawned;

    public static GameObject currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        // Search for first quest that isn't completed, then spawn it.
        SpawnNextQuest();
    }

    private void Update()
    {
        if (currentQuest.GetComponent<Quest>().Completed && questSpawned)
        {
            Destroy(currentQuest);
            questSpawned = false;

            SpawnNextQuest();
        }

        if (currentQuest.GetComponent<Quest>().questNumber == numberOfQuests && PlayerPrefs.GetInt("quest_" + numberOfQuests.ToString() + "_completed") == 1)
        {
            GameObject.Find("HUD_WinMessage").GetComponent<Text>().enabled = true;
            Invoke("ReturnToMainMenu", 5);
        }
    }

    void SpawnNextQuest()
    {
        for (int i = 1; i <= numberOfQuests; i++)
        {
            if (PlayerPrefs.GetInt("quest_" + i.ToString() + "_completed") == 0)
            {
                currentQuestNumber = i;
                GameObject quest = Resources.Load("Quests/Quest" + i.ToString() + "/Quest" + i.ToString()) as GameObject;
                currentQuest = Instantiate(quest, new Vector3(0, 0, 0), Quaternion.identity);
                currentQuest.name = "Quest" + i.ToString();
                HUD_Quest.QuestTitle = "Quest" + i.ToString();
                questSpawned = true;
                
                for (int i2 = 1; i2 <= currentQuest.GetComponent<Quest>().numberOfSteps; i2++)
                {
                    foreach (Transform child in currentQuest.transform)
                    {
                        if (child.gameObject.name == "Quest" + currentQuestNumber.ToString() + "_Step" + i2.ToString())
                        {
                            if (PlayerPrefs.GetInt("quest_" + currentQuestNumber.ToString() + "_step_" + i2.ToString() + "_completed") == 0)
                            {
                                HUD_Quest.StepDescription = child.GetComponent<Quest_Step>().description;
                                child.gameObject.SetActive(true);
                                return;
                            }
                            else if (PlayerPrefs.GetInt("quest_" + currentQuestNumber.ToString() + "_step_" + i2.ToString() + "_completed") == 1)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        print("All Quests Completed");
    }

    public static void SpawnCoins(int amount) // Spawns coins above player as quest reward
    {
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmountMin = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmountMax = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmount = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().SpawnCoins();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
