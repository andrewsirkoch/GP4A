using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip[] clips;
    public float minPitch;
    public float maxPitch;
    public float maxVolume = 1;

    public void play()
    {
        int randInt = Random.Range(0, clips.Length);
        gameObject.GetComponent<AudioSource>().clip = clips[randInt];
        gameObject.GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
        gameObject.GetComponent<AudioSource>().Play();
    }
}
