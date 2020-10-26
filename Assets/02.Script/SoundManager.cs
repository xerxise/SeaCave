using System;
using UnityEngine;

[Serializable]
public class SoundPackge
{
    public AudioClip Start;
    public AudioClip CraneDown;
    public AudioClip CraneUP;
    public AudioClip CrasheByWater;
    public AudioClip CrasheByShark;
    public AudioClip WhirlPool;
    public AudioClip Crashed;
    public AudioClip Ink;
    public AudioClip Shark;
    public AudioClip SubmarineTurnon;
    public AudioClip Dooropen;
    public AudioClip Electronic1;
    public AudioClip Electronic2;
    public AudioClip Glassbreak;
    public AudioClip Sonar;
    public AudioClip Warning;
    public AudioClip Deepsea;
    public AudioClip BrokenMachine;
    public AudioClip WarningSlow;
    public AudioClip CrasheBySharkEnginOff;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public SoundPackge Sounds;
    AudioSource[] Audio;

    void Awake()
    {
        Instance = this;
        Audio = GetComponents<AudioSource>();
    }

    public void PlayOneShot(AudioClip clip)
    {
        Audio[0].PlayOneShot(clip);
    }

    public void PlayLoop(AudioClip clip)
    {
        Audio[1].clip = clip;
        Audio[1].Play();
    }

    public void PauseLoop()
    {
        Audio[1].clip = null;
        Audio[1].Pause();
    }
}