using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomlyDropChildren : MonoBehaviour
{
    public float dropWaitTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            InvokeRepeating("randomDrop", 1, dropWaitTime);
        }
    }

    void randomDrop()
    {
        Rigidbody2D[] allChildren = GetComponentsInChildren<Rigidbody2D>();
        if (allChildren.Length > 0)
        {
            int decision = Random.Range(0, allChildren.Length);
            allChildren[decision].gravityScale = 1;
            allChildren[decision].AddForce(new Vector2(Random.Range(-10, 10), Random.Range(-5, -10)), ForceMode2D.Impulse);
            allChildren[decision].gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else if (allChildren.Length == 0)
        {
            CancelInvoke();
        }
    }
}
