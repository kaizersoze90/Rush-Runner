using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PlayerController player;
    [SerializeField] UIManager uiManager;
    [SerializeField] CinemachineManager camManager;
    [SerializeField] PaintWallManager paintManager;

    [Header("Settings")]
    [SerializeField] float victoryDelay;
    [SerializeField] float danceDelay;

    public static GameManager Instance { get; private set; }
    public bool IsGameActive { get; private set; }

    bool _isVictorious;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        IsGameActive = true;
    }

    void Update()
    {
        if (uiManager.GetPaintPercentage() >= 1f && !_isVictorious)
        {
            _isVictorious = true;

            AudioManager.Instance.PlayVictorySFX();

            ProcessReloadGame();
        }
    }

    public void ProcessReloadGame()
    {
        StartCoroutine(nameof(ReloadGame));
    }

    public void ProcessVictory()
    {
        StartCoroutine(nameof(Victory));
    }

    IEnumerator ReloadGame()
    {
        if (IsGameActive)
        {
            IsGameActive = false;

            AudioManager.Instance.PlayFailSFX();
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        IsGameActive = true;
    }

    IEnumerator Victory()
    {
        IsGameActive = false;

        DestroyOpponents();

        AudioManager.Instance.PlayVictorySFX();

        uiManager.HideRank();
        camManager.SetDanceCam();
        player.SetDanceAnim();

        yield return new WaitForSeconds(danceDelay);

        camManager.SetPaintCam();
        player.SetIdleAnim();

        yield return new WaitForSeconds(0.5f);

        paintManager.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        uiManager.SetPaintSlider();
    }

    void DestroyOpponents()
    {
        OpponentController[] opponents = FindObjectsOfType<OpponentController>();

        foreach (OpponentController opponent in opponents)
        {
            Destroy(opponent.gameObject);
        }
    }
}
