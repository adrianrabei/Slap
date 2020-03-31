using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource aSource;
    public AudioClip slap;
    public AudioClip wow;
    public AudioClip win;
    public AudioClip fail;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        aSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlaySound(name);
        }
    }

    public void PlaySound(string name)
    {
        if (PlayerPrefs.GetInt("sound", 1) == 0)
        {
            return;
        }

        if (name == "slap")
        {
            aSource.PlayOneShot(slap);
        }

        if (name == "wow")
        {
            aSource.PlayOneShot(wow);
        }

        if (name == "win")
        {
            aSource.PlayOneShot(win);
        }

        if (name == "fail")
        {
            aSource.PlayOneShot(fail);
        }
    }
}