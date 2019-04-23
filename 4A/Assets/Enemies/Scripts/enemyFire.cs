using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFire : MonoBehaviour
{
    [Space]
    public bool fire;
    private Animator animator;
    private enemyMovement movement;
    [Space]
    public GameObject weapon;
    [Space]
    public float attackDelay;
    [Header("In Frames")]
    public int animationDuration;
    [Space]
    public float weaponOffset;

    private bool startTimer;
    private float timer;
    [HideInInspector]
    public bool firing;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<enemyMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fire && movement.grounded && !firing)
        {
            if (fire && !firing) Invoke("Attack", attackDelay);
            fire = false;
            firing = true;
            movement.listen = false;
            animator.SetTrigger("FireWeapon");
            startTimer = true;
        }
        if (startTimer)
        {
            timer += 1;
            if (timer >= animationDuration && firing)
            {
                startTimer = false;
                firing = false;
                timer = 0;
                movement.listen = true;
                animator.SetTrigger("returnToIdle");
            }
        }
        else
        {
            timer = 0;
        }
    }
    void Attack()
    {
        if (GetComponentsInChildren<enemyMelee>().Length <= 1)
        {
            enemySound.swing(gameObject);

            Vector2 direction = new Vector2(0, 0);

            if (movement.flipX)
                direction = new Vector2(-weaponOffset, 0);
            else if (!movement.flipX)
                direction = new Vector2(weaponOffset, 0);

            GameObject projectile = Instantiate(weapon, new Vector2(transform.position.x + direction.x, transform.position.y), Quaternion.identity, transform) as GameObject;
        }  
    }
}
