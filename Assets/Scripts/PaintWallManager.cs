using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintWallManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    AudioSource _audioSource;
    Painter[] pieces;

    float _firstCount = 1;
    float _currentCount;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        pieces = GetComponentsInChildren<Painter>();

        _firstCount = pieces.Length;
    }

    public void IncreaseCount()
    {
        _currentCount++;

        _audioSource.pitch += 0.01f;
        _audioSource.Play();
    }

    public float CurrentProgress()
    {
        return _currentCount / _firstCount;
    }

    public void ShowPaintBrush()
    {
        canvas.gameObject.SetActive(true);
    }

    public void ActivateWall()
    {
        foreach (Painter piece in pieces)
        {
            piece.GetComponent<Painter>().enabled = true;
        }

        canvas.gameObject.SetActive(false);
    }
}
