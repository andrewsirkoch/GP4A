using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileFireball : MonoBehaviour
{
    [Space]
    public float speed;
    [HideInInspector]
    public bool exploded = false;
    [Space]
    public int damage;
    Animator animator;
    Vector2 direction;
    private bool damagedSomething;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        Invoke("Explode", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        exploded = true;
        animator.SetTrigger("Explode");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Invoke("DestroySelf", 0.5f);
        AudioManager.accessAudioSource("FireballBlast").play();
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && damagedSomething == false)
        {
            damagedSomething = true;
            collision.gameObject.GetComponent<enemyStats>().damageEnemy(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 2f), ForceMode2D.Impulse);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (collision.gameObject.tag != "Trigger" && exploded == false)
        {
            Explode();
        }
    }
}
