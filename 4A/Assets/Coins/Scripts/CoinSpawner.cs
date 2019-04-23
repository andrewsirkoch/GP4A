using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Attach this to an object that you want to spawn coins when it is destroyed

    public int coinAmountMin;
    public int coinAmountMax;
    public int coinAmount;

    public GameObject coin;

    public void Start()
    {
        coinAmount = Random.Range(coinAmountMin, coinAmountMax);
    }
    public void SpawnCoins()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            GameObject instantiatedCoin = Instantiate(coin, transform.position, Quaternion.identity) as GameObject;
            instantiatedCoin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 2f)), ForceMode2D.Impulse);
        }
    }
}
