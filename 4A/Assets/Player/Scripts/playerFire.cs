using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFire : MonoBehaviour
{
    private Animator animator;
    private playerMovement movement;

    public GameObject weapon;

    public float curveSpeed;

    private bool startTimer;
    private float timer;
    private bool firing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && movement.grounded && !firing)
        {
            firing = true;
            movement.listen = false;
            
            Invoke("fireProjectile", 0.9f);
            Invoke("PlayFireSound", 0.6f);
            animator.SetTrigger("FireWeapon");
            startTimer = true;
        }

        if (startTimer)
        {
            movement.anim_stopWalking();
            timer += 1;
            if (timer >= 60)
            {
                startTimer = false;
                firing = false;
                animator.SetTrigger("returnToIdle");
                movement.listen = true;
            }
        }
        else
        {
            timer = 0;
        }

        curveProjectile();
    }

    void fireProjectile()
    {
        
        Vector2 direction = new Vector2(0,0);

        if (movement.flipX)
            direction = new Vector2(-1.4f, 0);
        else if (!movement.flipX)
            direction = new Vector2(1.4f, 0);

        GameObject projectile = Instantiate(weapon, new Vector2(transform.position.x + direction.x, transform.position.y), Quaternion.identity, transform) as GameObject;

        projectile.transform.parent = null;

        projectile.GetComponent<Rigidbody2D>().AddForce(direction * projectile.GetComponent<projectileFireball>().speed, ForceMode2D.Impulse);
    }

    void curveProjectile()
    {
        GameObject[] allProjectiles = GameObject.FindGameObjectsWithTag("Projectile");

        if (allProjectiles != null && Input.GetMouseButton(0))
        {
            Vector3 direction = new Vector3(0, 0, 0);

            foreach (GameObject projectile in allProjectiles)
            {
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > projectile.transform.position.y)
                    direction = new Vector3(0, 1, 0);
                else
                    direction = new Vector3(0, -1, 0);

                if (!projectile.GetComponent<projectileFireball>().exploded)
                {
                    projectile.GetComponent<Rigidbody2D>().AddForce(direction * curveSpeed);
                }
            }
        }
    }
    
    void PlayFireSound()
    {
        AudioManager.accessAudioSource("FireballCast").play();
    }
}
