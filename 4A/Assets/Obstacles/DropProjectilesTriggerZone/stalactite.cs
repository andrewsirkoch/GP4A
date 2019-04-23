using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactite : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            // Hit the player!
            playerStats.damagePlayer(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemyStats>().damageEnemy(damage);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Ground")
        {
            // Hit the ground!
            Destroy(this.gameObject);
        }
    }
}
