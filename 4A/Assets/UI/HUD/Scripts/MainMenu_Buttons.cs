using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGameScene()
    {
        if (PlayerPrefs.GetInt("quest_3_completed") == 1)
        {
            ClearQuestHistory();
        }
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void ClearQuestHistory()
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

        playerStats.coins = 0;
    }
}
