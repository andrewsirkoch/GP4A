using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroySelf", 15);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerStats.increaseCoins(1);
            PlayerPrefs.SetInt("coinsObtained", PlayerPrefs.GetInt("coinsObtained") + 1);
            AudioManager.accessAudioSource("CoinPickup").play();
            Destroy(this.gameObject);
        }
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
