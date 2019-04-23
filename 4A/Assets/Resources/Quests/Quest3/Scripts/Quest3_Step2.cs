using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest3_Step2 : MonoBehaviour
{
    // TALKING TO GUY AFTER KILLING BOARS

    public int numOfKnocks;
    public bool manAtBalcony = false;
    public bool chatRunning = false;
    public bool chatRan = false;
    public bool Completed = false;

    public void FixedUpdate()
    {
        if (numOfKnocks >= 5 && !manAtBalcony && !chatRan)
        {
            MoveGuyOut();
        }
        if (numOfKnocks >= 5 && manAtBalcony)
        {
            chatRunning = true;
        }
        if (numOfKnocks >= 5 && manAtBalcony && chatRunning)
        {
            SpawnChatBox();
            AudioManager.accessAudioSource("gibberish").play();
        }
        if (numOfKnocks >= 5 && manAtBalcony && chatRan)
        {
            MoveGuyIn();
            AudioManager.accessAudioSource("gibberish").gameObject.GetComponent<AudioSource>().Stop();
        }
        if (numOfKnocks >= 5 && !manAtBalcony && chatRan)
        {
            PlayerPrefs.SetInt("quest_3_step_2_completed", 1);
            Completed = true;
            gameObject.GetComponent<LastStep>().Completed = true;

        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("quest_3_step_1_completed") == 1 && Input.GetKeyUp(KeyCode.W) && collision.gameObject.name == "Player")
        {
            numOfKnocks += 1;
            AudioManager.accessAudioSource("DoorKnock").play();
        }
    }

    public void MoveGuyOut()
    {
        Transform man = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
        if (man.position.x >= 53) man.position -= new Vector3(0.03f, 0, 0);
        else if (man.position.x < 53) manAtBalcony = true;
    }

    public void MoveGuyIn()
    {
        Transform man = gameObject.GetComponentInChildren<SpriteRenderer>().transform;
        man.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        man.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        if (man.position.x <= 57) man.position += new Vector3(0.03f, 0, 0);
        else if (man.position.x > 57) manAtBalcony = false;
    }

    public void SpawnChatBox()
    {
        if (chatRunning)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "man")
                {
                    child.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
                }
                if (child.name == "questIndicator")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.name == "buildingMask")
                {
                    child.GetComponent<SpriteMask>().enabled = false;
                }
                if (child.name == "ChatBubble")
                {
                    child.gameObject.SetActive(true);
                    foreach (Transform childOfChild in child)
                    {
                        if (childOfChild.name == "chat")
                        {
                            foreach (Transform childOfChildOfChild in child)
                            {
                                if (childOfChildOfChild.name == "mask")
                                {
                                    if (childOfChildOfChild.transform.localScale.x < 1.75f)
                                    {
                                        childOfChildOfChild.transform.localScale = new Vector3(childOfChildOfChild.transform.localScale.x + 0.02f, childOfChildOfChild.transform.localScale.y, childOfChildOfChild.transform.localScale.z);
                                    }
                                    else
                                    {
                                        ShiftTextUp(childOfChild.gameObject);
                                        childOfChildOfChild.transform.localScale = new Vector3(0, childOfChildOfChild.transform.localScale.y, childOfChildOfChild.transform.localScale.z);
                                    }
                                }
                            }
                            if (childOfChild.localPosition.y >= 5)
                            {
                                chatRunning = false;
                                child.gameObject.SetActive(false);
                                chatRan = true;
                                GameObject.Find("buildingMask").GetComponent<SpriteMask>().enabled = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ShiftTextUp(GameObject text)
    {
        text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + 0.37f, text.transform.position.z);
    }
}
