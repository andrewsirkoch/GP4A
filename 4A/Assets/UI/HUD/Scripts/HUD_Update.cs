using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Update : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        UpdateCoins();
    }

    void UpdateHealth()
    {
        GameObject HealthBar = GameObject.Find("HealthBar");
        float conversionFactor = playerStats.health * (1f/100f);
        HealthBar.transform.localScale = new Vector3(conversionFactor, 1, 1);
    }

    void UpdateCoins()
    {
        GameObject Coins = GameObject.Find("CoinDisplay");
        Coins.GetComponent<Text>().text = playerStats.coins.ToString();
    }
}
