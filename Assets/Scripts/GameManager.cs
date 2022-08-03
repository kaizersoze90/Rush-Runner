using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] UIManager uiManager;
    [SerializeField] CinemachineManager camManager;
    [SerializeField] PaintWallManager paintManager;
    [SerializeField] float victoryDelay;
    [SerializeField] float danceDelay;

    public static GameManager Instance { get; private set; }
    public bool IsGameActive { get; private set; }

    void Start()
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

    public void ProcessReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        IsGameActive = true;
    }

    public void ProcessVictory()
    {
        StartCoroutine(nameof(Victory));
    }

    IEnumerator Victory()
    {
        IsGameActive = false;
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
}
