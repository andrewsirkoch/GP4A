using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
    // Attach this to a GameObject with a trigger.

    [Space]
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroySelf", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            playerStats.damagePlayer(damage);
        }
    }

    private void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
