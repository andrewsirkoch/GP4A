using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class darkZone : MonoBehaviour
{
    public GameObject vignette;

    public Vector2 size;
    [Header("Which side does the player enter from?")]
    public bool right;
    public bool left;
    public bool bottom;
    public bool top;

    [Space]
    public float playerPosNormalized;

    private Vector2 rightSidePos;
    private Vector2 leftSidePos;
    private Vector2 topSidePos;
    private Vector2 bottomSidePos;
    private Image vignetteSprite;

    private float x1;
    private float x2;
    private float y1;
    private float y2;

    private float slope;
    private float b;

    // Start is called before the first frame update
    void Start()
    {
        rightSidePos = new Vector2(transform.position.x + size.x / 2, transform.position.y);
        leftSidePos = new Vector2(transform.position.x - size.x / 2, transform.position.y);
        topSidePos = new Vector2(transform.position.x, transform.position.y + size.y / 2);
        bottomSidePos = new Vector2(transform.position.x, transform.position.y - size.y / 2);

        vignetteSprite = vignette.GetComponent<Image>();

        if (right)
        {
            x1 = rightSidePos.x;
            x2 = leftSidePos.x;
        }
        if (left)
        {
            x1 = leftSidePos.x;
            x2 = rightSidePos.x;
        }
        if (top)
        {
            x1 = topSidePos.y;
            x2 = bottomSidePos.y;
        }
        if (bottom)
        {
            x1 = bottomSidePos.y;
            x2 = topSidePos.y;
        }

        y1 = 0.01f;
        y2 = 1f;
        slope = (y2 - y1) / (x2 - x1);
        b = y1 - (slope * x1);

    }

    // Update is called once per frame
    void Update()
    {
        if (returnPlayerPos().x <= rightSidePos.x && returnPlayerPos().x >= leftSidePos.x && returnPlayerPos().y <= topSidePos.y && returnPlayerPos().y >= bottomSidePos.y)
        {
            if (right || left)
            {
                playerPosNormalized = (slope * returnPlayerPos().x) + b;
            }
            else if (top || bottom)
            {
                playerPosNormalized = (slope * returnPlayerPos().y) + b;
            }
            
            vignetteSprite.color = new Color(vignetteSprite.color.r, vignetteSprite.color.g, vignetteSprite.color.b, playerPosNormalized);
        }

        if (right)
        {
            if (returnPlayerPos().x > rightSidePos.x)
            {
                playerPosNormalized = 0;
            }
            else if (returnPlayerPos().x < leftSidePos.x)
            {
                playerPosNormalized = 1;
            }
        }
        if (left)
        {
            if (returnPlayerPos().x < leftSidePos.x)
            {
                playerPosNormalized = 0;
            }
            else if (returnPlayerPos().x > rightSidePos.x)
            {
                playerPosNormalized = 1;
            }
        }
        if (top)
        {
            if (returnPlayerPos().y > topSidePos.y)
            {
                playerPosNormalized = 0;
            }
            else if (returnPlayerPos().y < bottomSidePos.y)
            {
                playerPosNormalized = 1;
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x, size.y, 2));
    }

    Vector2 returnPlayerPos()
    {
        GameObject player = GameObject.Find("Player");
        return new Vector2(player.transform.position.x, player.transform.position.y);
    }
}
