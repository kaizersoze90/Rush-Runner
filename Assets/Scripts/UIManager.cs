using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PaintWallManager wallManager;
    [SerializeField] Slider paintedSlider;

    bool _isVictorious;

    void Update()
    {
        paintedSlider.value = wallManager.CurrentProgress();

        if (paintedSlider.value >= 1f && !_isVictorious)
        {
            _isVictorious = true;
            StartCoroutine(nameof(ProcessVictory));
        }
    }

    public void SetPaintSlider()
    {
        paintedSlider.gameObject.SetActive(true);
    }

    IEnumerator ProcessVictory()
    {
        AudioManager.Instance.PlayVictorySFX();

        yield return new WaitForSeconds(1f);

        GameManager.Instance.ProcessReloadGame();
    }
}
