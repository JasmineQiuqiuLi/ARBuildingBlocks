using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public static AudioManager instance;
    public AudioClip tap,manipulation;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    public void ToggleAudio(bool status)
    {
        audioSource.mute = !status;

    }

    public void PlayUITap()
    {
        audioSource.PlayOneShot(tap);
    }

    public void PlayManipulationSound()
    {
        audioSource.PlayOneShot(manipulation);
    }
}
