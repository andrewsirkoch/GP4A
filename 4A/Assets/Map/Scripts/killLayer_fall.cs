using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killLayer_fall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject camera = collision.gameObject.GetComponentInChildren<Camera>().gameObject;

            camera.transform.parent = null;

            playerStats.Control(false);
            GameObject.Find("Player").GetComponent<playerStats>().Die("fall");
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemyStats>().Die();
        }
    }
}
