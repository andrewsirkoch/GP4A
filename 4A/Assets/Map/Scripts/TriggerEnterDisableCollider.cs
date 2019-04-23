using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterDisableCollider : MonoBehaviour
{
    public Collider2D[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }
}
