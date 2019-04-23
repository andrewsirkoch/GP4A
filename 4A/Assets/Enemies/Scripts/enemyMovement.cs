using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [Space]
    public float walkSpeed;
    [Space]
    public bool grounded;
    [Space]
    public bool walk;
    [HideInInspector]
    public bool walking;
    [HideInInspector]
    public bool flipX;
    [Space]
    public float rayCastOffset;
    [Space]
    public float rayCastOffsetVertical;
    [Space]
    public float rayCastOffsetHorizontal;
    [Space]
    public float eyeLevel;
    [Space]
    public float jumpPower;

    private enemyFire enemyFire;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    private Animator animator;
    private int timer;
    private float value;
    private Vector3 direction = new Vector3(1, 0, 0);
    private int jumpTimer;
    private int switchDirectionTimer;

    [HideInInspector]
    public bool listen = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        enemyFire = GetComponent<enemyFire>();
        listen = true;

    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer += 1;
        switchDirectionTimer += 1;
        flipX = sprite.flipX;

        // Player Detected Right Side
        if (RightCheck() == 1 || TopRightCheck() == 1)
        {
            walk = true;
            direction = new Vector3(1, 0, 0);
        }

        // Player Detected Left Side
        else if (LeftCheck() == 1 || TopLeftCheck() == 1)
        {
            walk = true;
            direction = new Vector3(-1, 0, 0);
        }

        // No Player Detected, Engage Random Walk =====> Initialize...
        else if (LeftCheck() == 0 && RightCheck() == 0 && TopLeftCheck() == 0 && TopRightCheck() == 0)
        {
            timer += 1;
            if (timer >= 100)
            {
                value = Random.value;
                timer = 0;

                if (value >= 0.75f)
                { // Switch Direction
                    walk = true;
                    direction = new Vector3(direction.x * -1, 0, 0);
                }
                else if (value <= 0.33f)
                { // Stop and do nothing
                    walk = false;
                }
                else if (value > 0.33f && value < 0.75f)
                { // Just keep going
                    walk = true;
                }
            }
        }
        else if (LeftCheck() == 3 || RightCheck() == 3 && !enemyFire.firing)
        {
            enemyFire.fire = true;
        }
        // Ran into wall on right side, jump, wait two seconds, then flip to left
        if (LeftCheck() == 0 && RightCheck() == 2 && TopRightCheck() != 2)
        {
            Jump();
            walk = true;
            Invoke("SwitchDirection", 2);
        }
        // Ran into wall on left side, jump, wait two seconds, then flip to right
        else if (LeftCheck() == 2 && RightCheck() == 0 && TopLeftCheck() != 2)
        {
            print("Jump!");
            Jump();
            walk = true;
            Invoke("SwitchDirection", 2);
        }

        else if (LeftCheck() == 0 && RightCheck() == 2 && TopRightCheck() == 2)
        {
            SwitchDirection();
        }

        else if (RightCheck() == 0 && LeftCheck() == 2 && TopLeftCheck() == 2)
        {
            SwitchDirection();
        }

        if (LeftEdgeCheck() == 1 && LeftCheck() == 0 && RightEdgeCheck() == 0 && direction.x == -1)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (RightEdgeCheck() == 1 && RightCheck() == 0 && LeftEdgeCheck() == 0 && direction.x == 1)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else if (LeftEdgeCheck() == 1 && RightEdgeCheck() == 1)
        {
            // Do Nothing, we're falling!
        }

        // Flipping Sprite
        if (direction.x == -1)
            sprite.flipX = true;
        else if (direction.x == 1)
            sprite.flipX = false;

        // Determining Grounded Boolean

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - rayCastOffsetVertical), -Vector2.up, 0.001f);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - rayCastOffsetVertical, transform.position.z), -Vector2.up, Color.white);
        if (hit.collider == null || hit.collider.gameObject.tag != "Ground" && listen)
        {
            anim_stopWalking();
            grounded = false;
        }
        else if (hit.collider != null && hit.collider.gameObject.tag == "Ground")
        {
            grounded = true;
        }


        // Movement
        if (walk)
            transform.position += direction * walkSpeed * Time.deltaTime;

        // Listen is disabled when attack animation is engaged, prevents conflicting animations.
        if (listen && walk)
        {
            if (grounded == true)
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
        else if (listen && !walk)
        {
            anim_stopWalking();
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
        if (listen) animator.SetBool("walking", true);
    }

    public void anim_stopWalking()
    {
        if (listen) animator.SetBool("walking", false);
    }

    void anim_startJumping()
    {
        if (listen) animator.SetBool("grounded", false);
    }

    void anim_stopJumping()
    {
        if (listen) animator.SetBool("grounded", true);
    }

    public int RightCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset, transform.position.y + rayCastOffsetHorizontal), Vector2.right, 20f);
        Debug.DrawRay(new Vector3(transform.position.x + rayCastOffset, transform.position.y + rayCastOffsetHorizontal, transform.position.z), Vector2.right * 20, Color.red);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player" && hit.distance <= 0.25f)
            return 3;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            return 1;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Ground" && hit.distance <= 0.25f)
            return 2;

        else if (hit.collider == null || hit.collider.gameObject.tag != "Player")
            return 0;
        
        return 0;
    }

    public int LeftCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset, transform.position.y + rayCastOffsetHorizontal), -Vector2.right, 20f);
        Debug.DrawRay(new Vector3(transform.position.x - rayCastOffset, transform.position.y + rayCastOffsetHorizontal, transform.position.z), -Vector2.right * 20, Color.red);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player" && hit.distance <= 0.25f)
            return 3;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            return 1;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Ground" && hit.distance <= 0.25f)
            return 2;

        else if (hit.collider == null || hit.collider.gameObject.tag != "Player")
            return 0;

        return 0;
    }

    public int TopLeftCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset, transform.position.y + eyeLevel), -Vector2.right, 20f);
        Debug.DrawRay(new Vector3(transform.position.x - rayCastOffset, transform.position.y + eyeLevel, transform.position.z), -Vector2.right * 20, Color.green);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player" && hit.distance <= 0.25f)
            return 3;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            return 1;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Ground" && hit.distance <= 0.25f)
            return 2;

        else if (hit.collider == null || hit.collider.gameObject.tag != "Player")
            return 0;

        return 0;
    }

    public int TopRightCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset, transform.position.y + eyeLevel), Vector2.right, 20f);
        Debug.DrawRay(new Vector3(transform.position.x + rayCastOffset, transform.position.y + eyeLevel, transform.position.z), Vector2.right * 20, Color.green);

        if (hit.collider != null && hit.collider.gameObject.tag == "Player" && hit.distance <= 0.25f)
            return 3;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            return 1;

        else if (hit.collider != null && hit.collider.gameObject.tag == "Ground" && hit.distance <= 0.25f)
            return 2;

        else if (hit.collider == null || hit.collider.gameObject.tag != "Player")
            return 0;

        return 0;
    }

    public int LeftEdgeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset, transform.position.y), -Vector2.up, 4);
        Debug.DrawRay(new Vector3(transform.position.x - rayCastOffset, transform.position.y, transform.position.z), -Vector2.up * 4, Color.blue);

        if (hit.collider == null)
            return 1;
        return 0;
    }

    public int RightEdgeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset, transform.position.y), -Vector2.up, 4);
        Debug.DrawRay(new Vector3(transform.position.x + rayCastOffset, transform.position.y, transform.position.z), -Vector2.up * 4, Color.blue);

        if (hit.collider == null)
            return 1;
        return 0;
    }

    public void Jump()
    {
        if (jumpTimer >= 100)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpTimer = 0;
        }
        
    }

    public void SwitchDirection()
    {
        if (switchDirectionTimer >= 100)
        {
            direction = new Vector3(direction.x * -1, 0, 0);
            switchDirectionTimer = 0;
        }
    }
}
