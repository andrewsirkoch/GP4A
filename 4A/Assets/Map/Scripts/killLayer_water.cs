using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killLayer_water : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.Control(false);
            GameObject.Find("Player").GetComponent<playerStats>().Die("water");
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemyStats>().Die();
        }
    }
}
