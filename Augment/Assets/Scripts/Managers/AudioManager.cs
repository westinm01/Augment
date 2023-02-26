using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource fxSource;

    public void PlaySound(AudioClip clip)
    {
        fxSource.PlayOneShot(clip);
    }

    public bool IsPlayingFX()
    {
        return fxSource.isPlaying;
    }
}
