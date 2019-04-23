using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int numberOfQuests;
    public int currentQuestNumber;
    public static bool questSpawned;

    public GameObject currentQuest;

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
                questSpawned = true;
                
                for (int i2 = 1; i2 <= currentQuest.GetComponent<Quest>().numberOfSteps; i2++)
                {
                    print(i2);
                    foreach (Transform child in currentQuest.transform)
                    {
                        if (child.gameObject.name == "Quest" + currentQuestNumber.ToString() + "_Step" + i2.ToString())
                        {
                            if (PlayerPrefs.GetInt("quest_" + currentQuestNumber.ToString() + "_step_" + i2.ToString() + "_completed") == 0)
                            {
                                child.gameObject.SetActive(true);
                                return;
                            }
                            else if (PlayerPrefs.GetInt("quest_" + currentQuestNumber.ToString() + "_step_" + i2.ToString() + "_completed") == 1)
                            {
                                print("disabling step");
                                print(child.gameObject.name);
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public static void SpawnCoins(int amount) // Spawns coins above player as quest reward
    {
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmountMin = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmountMax = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().coinAmount = amount;
        GameObject.Find("PlayerCoinSpawner").GetComponent<CoinSpawner>().SpawnCoins();
    }
}
