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

    public void Update()
    {
        playerHealth = health;
        playerCoins = coins;
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
        if (type == "water")
        {
            GameObject.Find("HUD_deathMessage_water").GetComponent<Text>().enabled = true;
        }
        else if (type == "fall")
        {
            GameObject.Find("HUD_deathMessage_fall").GetComponent<Text>().enabled = true;
        }

        Invoke("reloadScene", 5);
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
