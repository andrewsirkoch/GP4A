using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySound : MonoBehaviour
{
    public static void death(GameObject enemy)
    {
        if (enemy.GetComponent<enemyStats>().enemyName == "boar")
        {
            AudioManager.accessAudioSource("boarDeath").play();
        }
    }

    public static void hit(GameObject enemy)
    {
        if (enemy.GetComponent<enemyStats>().enemyName == "boar")
        {
            AudioManager.accessAudioSource("boarHit").play();
        }
    }

    public static void swing(GameObject enemy)
    {
        if (enemy.GetComponent<enemyStats>().enemyName == "boar")
        {
            AudioManager.accessAudioSource("boarSwing").play();
        }
        else if (enemy.GetComponent<enemyStats>().enemyName == "goblin")
        {
            AudioManager.accessAudioSource("goblinSwing").play();
        }
    }
}
