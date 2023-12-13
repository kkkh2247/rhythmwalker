using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager inst;

    public AudioSource bgmSound;
    public AudioSource effectSound;
    public AudioSource punchsound;
    public AudioSource gethit;
    public AudioSource bikeSound;
    public AudioSource getOutsideSound;
    public AudioSource titleSound;
    public AudioSource tutorialSound;
    public AudioSource bringiton;

    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this.gameObject);
    }

    public void Playsound()
    {
        bgmSound.Play();
    }

    public void Stopsound()
    {
        bgmSound.Stop();
    }

    public void Pausesound()
    {
        bgmSound.Pause();
    }

    public void Stoptitlesound()
    {
        titleSound.Stop();
    }

    public void Playtitlesound()
    {
        titleSound.Play();
    }

    public void Playtutorialsound()
    {
        tutorialSound.Play();
    }

    public void Stoptutorialsound()
    {
        tutorialSound.Stop();
    }
}
