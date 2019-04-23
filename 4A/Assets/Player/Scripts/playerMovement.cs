using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Space]
    public float walkSpeed;
    [Space]
    public float jumpSpeed;
    [Space]
    public bool grounded;
    [Space]
    public bool walking;

    public bool flipX;

    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool jumpedTwice;

    [HideInInspector]
    public bool listen = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        listen = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        flipX = sprite.flipX;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        // Flipping Sprite
        if (direction.x == -1 || Input.GetKey(KeyCode.A))
            sprite.flipX = true;
        else if (direction.x == 1 || Input.GetKey(KeyCode.D))
            sprite.flipX = false;

        // Determining Grounded Boolean

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.1f), -Vector2.up, 0.001f);
        if (hit.collider == null)
        {
            anim_stopWalking();
            grounded = false;
        }
        else if (hit.collider != null && (hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Enemy"))
        {
            grounded = true;
            jumpedTwice = false;
        }


        // Movement
        transform.position += direction * walkSpeed * Time.deltaTime;
        if (listen)
        {
            if (Input.GetAxis("Horizontal") != 0 && grounded == true)
            {
                anim_startWalking();
                walking = true;
            }
            else
            {
                anim_stopWalking();
                walking = false;
            }
        }
        

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            anim_stopWalking();
            rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }

        // Double Jumping
        if (Input.GetKeyDown(KeyCode.Space) && jumpedTwice == false && grounded == false)
        {
            jumpedTwice = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            GameObject.Find("doubleJump").GetComponent<ParticleSystem>().Play();
        }

        // Jumping Animation
        if (listen && grounded == false)
        {
            anim_startJumping();
        }
        else if (listen)
        {
            anim_stopJumping();
        }
    }

    void anim_startWalking()
    {
        animator.SetBool("walking", true);
    }

    public void anim_stopWalking()
    {
        animator.SetBool("walking", false);
    }

    void anim_startJumping()
    {
        animator.SetBool("grounded", false);
    }

    void anim_stopJumping()
    {
        animator.SetBool("grounded", true);
    }
}
