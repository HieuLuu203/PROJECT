using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    public AudioSource audioSourceSound;
    public AudioSource audioSourceMusic;
    public AudioSource audioSourceMeme;

    [Header("AudioClips - Sound")]
    public AudioClip audioClip_ButtonConfirm;

    [Header("AudioClips - Music")]
    public AudioClip audioClip_BackgroundMusic;




    public static AudioManager instance {  get; private set; }
    public void Awake()
    {
        instance = this; 

    }
    public void Start()
    {
        audioSourceMusic.loop = true;
        audioSourceMeme.loop = true;
        PlayMusic(audioClip_BackgroundMusic);
    }
    public void PlaySound(AudioClip audioClip)
    {
        audioSourceSound.PlayOneShot(audioClip);
    }
    public void PlayMusic(AudioClip audioClip)
    {
        audioSourceMusic.clip = audioClip;
        audioSourceMusic.Play();
    }

    public void PlayMemeAudio(AudioClip audioClip)
    {
        audioSourceMeme.clip = audioClip;
        audioSourceMeme.Play();
    }

    public void PauseMemeAudio()
    {
        audioSourceMeme.Pause();
    }

}
