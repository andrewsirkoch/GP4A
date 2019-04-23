using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyTeleport : MonoBehaviour
{
    [Header("Initiated by W or S?")]
    public bool W;
    public bool S;

    [Space]
    [Header("Location to be teleported to")]
    public Vector3 location;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (W && !S)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    collision.gameObject.transform.position = location;
                }
            }
            else if (S && !W)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    collision.gameObject.transform.position = location;
                }
            }
        }
    }
}
