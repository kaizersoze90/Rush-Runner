using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Effects")]
    [SerializeField] AudioClip victorySFX;
    [SerializeField] AudioClip failSFX;

    AudioSource _audioSource;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayVictorySFX()
    {
        _audioSource.PlayOneShot(victorySFX);
    }

    public void PlayFailSFX()
    {
        _audioSource.PlayOneShot(failSFX);
    }
}
