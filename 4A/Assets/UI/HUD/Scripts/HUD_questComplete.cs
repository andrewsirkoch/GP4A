using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_questComplete : MonoBehaviour
{
    public void flashMessage()
    {
        GameObject.Find("HUD_QuestComplete").GetComponent<Text>().enabled = true;
        Invoke("disable", 3);
    }

    public void disable()
    {
        GameObject.Find("HUD_QuestComplete").GetComponent<Text>().enabled = false;
    }
}
