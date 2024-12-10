using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script to control the background music
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    //Start music
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    //Stop Music
    public void StopMusic()
    {
        audioSource.Stop();
    }

    //Volume in game
    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume; 
    }
}
