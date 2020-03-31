using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform settings, soundVibroWindow;
    [SerializeField] private GameObject soundOff, vibroOff;
    void Start()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            soundOff.SetActive(false);
        }
        else
        {
            soundOff.SetActive(true);
        }

        if (PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            vibroOff.SetActive(false);

        }
        else
        {
            vibroOff.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void ToSettings()
    {
        settings.DOAnchorPos(new Vector2(0, 0), 0.35f);
        soundVibroWindow.DOShakeScale(0.6f);
    }

    public void Back()
    {
        settings.DOAnchorPos(new Vector2(-1220, 0), 0.35f);
    }

    public void SoundOnOff()
    {
        if (PlayerPrefs.GetInt("sound", 1) == 1)
        {
            PlayerPrefs.SetInt("sound", 0);
            Debug.Log("Sound off");
            if (soundOff.activeSelf)
            {
                soundOff.SetActive(false);
            }
            else
            {
                soundOff.SetActive(true);
            }
    }
        else
        {
            PlayerPrefs.SetInt("sound", 1);
            Debug.Log("Sound on");
            if (!soundOff.activeSelf)
            {
                soundOff.SetActive(true);
            }
            else
            {
                soundOff.SetActive(false);
            }
        }

    }

    public void VibrationOnOff()
    {
        if (PlayerPrefs.GetInt("vibro", 1) == 1)
        {
            vibroOff.SetActive(true);
            PlayerPrefs.SetInt("vibro", 0);
            Debug.Log("Vibro off");
        }
        else
        {
            vibroOff.SetActive(false);
            PlayerPrefs.SetInt("vibro", 1);
            Debug.Log("Vibro on");
        }

    }
}
