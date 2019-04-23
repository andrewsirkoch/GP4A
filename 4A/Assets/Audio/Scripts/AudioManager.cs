using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int totalSongs;
    private int songNumber;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAudio();

        songNumber = Random.Range(1, totalSongs + 1);
        PlaySong(songNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            if (songNumber == totalSongs)
            {
                songNumber = 1;
                PlaySong(songNumber);
            }
            else
            {
                songNumber++;
                PlaySong(songNumber);
            }
        }
    }

    public static PlaySound accessAudioSource(string name)
    {
        foreach (Transform child in GameObject.Find("AudioManager").transform)
        {
            if (child.name == name)
            {
                return child.gameObject.GetComponent<PlaySound>();
            }
        }
        return GameObject.Find("AudioManager").GetComponent<PlaySound>();
    }

    public void UpdateAudio()
    {
        float music = PlayerPrefs.GetFloat("volume_music");
        float sfx = PlayerPrefs.GetFloat("volume_sfx");
        float amb = PlayerPrefs.GetFloat("volume_amb");

        foreach (Transform child in transform)
        {
            float maxVolume = child.gameObject.GetComponent<PlaySound>().maxVolume;
            float multiplier = 1;

            AudioSource audioSource = child.gameObject.GetComponent<AudioSource>();

            if (child.gameObject.tag == "audio_music") multiplier = PlayerPrefs.GetFloat("volume_music");
            else if (child.gameObject.tag == "audio_SFX") multiplier = PlayerPrefs.GetFloat("volume_sfx");
            else if (child.gameObject.tag == "audio_ambient") multiplier = PlayerPrefs.GetFloat("volume_amb");

            audioSource.volume = maxVolume * multiplier;
        }

        float vol = gameObject.GetComponent<PlaySound>().maxVolume;
        float mult = PlayerPrefs.GetFloat("volume_music");
        gameObject.GetComponent<AudioSource>().volume = vol * mult;

    }

    void PlaySong(int number)
    {
        AudioClip Clip = Resources.Load<AudioClip>("Songs/" + number.ToString());
        gameObject.GetComponent<AudioSource>().clip = Clip;
        gameObject.GetComponent<AudioSource>().Play();
    }
}