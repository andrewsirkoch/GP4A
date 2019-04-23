using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStats : MonoBehaviour
{
    public string enemyName;
    public int health;

    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive)
        {
            Die();
            alive = false;
        }
    }

    public void damageEnemy(int damage)
    {
        enemySound.hit(gameObject);
        if (health - damage >= 0)
        {
            health -= damage;
        }
        else if (health - damage < 0)
        {
            health = 0;
        }
    }

    public void Die()
    {
        enemySound.death(gameObject);
        gameObject.GetComponent<CoinSpawner>().SpawnCoins();
        enemyMovement enemyMovement = gameObject.GetComponent<enemyMovement>();
        gameObject.GetComponent<enemyFire>().enabled = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().simulated = false;
        enemyMovement.anim_stopWalking();
        enemyMovement.grounded = true;
        enemyMovement.enabled = false;

        Animator animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetTrigger("returnToIdle");
        animator.SetTrigger("Die");
        Invoke("DestroySelf", 3);

        
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
