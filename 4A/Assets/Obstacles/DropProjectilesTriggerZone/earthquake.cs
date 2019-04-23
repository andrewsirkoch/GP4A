using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earthquake : MonoBehaviour
{
    public int screenShakeLength;

    private int timesShook = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            InvokeRepeating("CameraShake", 1, 2.5f);
            AudioManager.accessAudioSource("earthquake").play();
        }
    }

    void CameraShake()
    {
        if (timesShook < screenShakeLength)
        {
            CameraFunct.Shake();
            timesShook += 1;
        }
        else if (timesShook >= screenShakeLength)
        {
            CancelInvoke();
        }
    }
}
