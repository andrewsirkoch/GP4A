using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void returnToIdle()
    {
        GetComponent<Animator>().SetTrigger("returnToIdle");
    }

    public static void Shake()
    {
        GameObject.Find("playerCamera").GetComponent<Animator>().SetTrigger("shake");
    }
}
