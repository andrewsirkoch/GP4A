using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerStats : MonoBehaviour
{
    public static int health = 100;
    public static int coins;

    // Only for view in editor -- do not edit these values.
    public int playerHealth;
    public int playerCoins;
    public void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            Control(true);
            playerHealth = 100;
            health = 100;
            GameObject.Find("HUD_deathMessage_killed").GetComponent<Text>().enabled = false;
        }
    }
    public void Start()
    {
        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetInt("coins", 0);
        if (!PlayerPrefs.HasKey("coinsObtained")) PlayerPrefs.SetInt("coinsObtained", 0);
        if (!PlayerPrefs.HasKey("highscore")) PlayerPrefs.SetInt("highscore", 0);
        if (!PlayerPrefs.HasKey("enemiesKilled")) PlayerPrefs.SetInt("enemiesKilled", 0);
        Control(true);
        coins = PlayerPrefs.GetInt("coins");
    }
    public void Update()
    {
        playerHealth = health;
        playerCoins = coins;
        PlayerPrefs.SetInt("coins", playerCoins);
        if (PlayerPrefs.GetInt("coins") > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", playerCoins);
        }

        if (playerHealth <= 0)
        {
            Die("killed");
        }
    }

    public static void damagePlayer(int damage)
    {
        AudioManager.accessAudioSource("playerHurt").play();

        if (health - damage >= 0)
        {
            health -= damage;
        }
        else if (health - damage < 0)
        {
            health = 0;
        }
    }

    public static void UsePotion()
    {
        if (coins - 10 >= 0)
        {
            if (health + 25 <= 100)
            {
                health += 25;
            }
            else if (health + 25 > 100)
            {
                health = 100;
            }
            coins -= 10;
        }
    }

    public static void increaseCoins(int amount)
    {
        coins += amount;
    }

    public static void Control(bool condition)
    {
        GameObject.Find("Player").GetComponent<playerFire>().enabled = condition;
        GameObject.Find("Player").GetComponent<playerMovement>().enabled = condition;
    }

    public void Die(string type)
    {
        Control(false);
        if (type == "water")
        {
            GameObject.Find("HUD_deathMessage_water").GetComponent<Text>().enabled = true;
        }
        else if (type == "fall")
        {
            GameObject.Find("HUD_deathMessage_fall").GetComponent<Text>().enabled = true;
        }
        else if (type == "killed")
        {
            GameObject.Find("HUD_deathMessage_killed").GetComponent<Text>().enabled = true;
        }

        Invoke("reloadScene", 5);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
